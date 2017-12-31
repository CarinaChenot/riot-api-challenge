using System;
using System.Collections.Generic;

namespace LolAPI
{
    internal class RateLimiter
    {
        private readonly Dictionary<string, RateLimiter> _rateLimiters = new Dictionary<string, RateLimiter>();
        private readonly DateTime[] _resetDate = new DateTime[6];
        private readonly bool[] _reached = new bool[6];

        public RateLimiter Get(string name)
        {
            if (name == null) return null;

            if(!_rateLimiters.ContainsKey(name))
                _rateLimiters.Add(name, new RateLimiter());

            return _rateLimiters[name];
        }

        public void Read(string rateLimitHeader, string rateLimitCountHeader)
        {
            string[] appRateLimit = rateLimitHeader.Split(',');
            string[] appRateLimitCount = rateLimitCountHeader.Split(',');

            for (int i = 0; i < appRateLimit.Length; i++)
            {
                int currentCallNumber = int.Parse(appRateLimitCount[i].Split(':')[0]);
                int maxCallNumber = int.Parse(appRateLimit[i].Split(':')[0]);
                int timespan = int.Parse(appRateLimit[i].Split(':')[1]);

                //First call, so we keep the date in order to know when the span reset
                if (currentCallNumber == 1 || _resetDate[i] == DateTime.MinValue)
                {
                    _reached[i] = _resetDate[i] == DateTime.MinValue && currentCallNumber !=  1;
                    _resetDate[i] = DateTime.Now.AddSeconds(timespan);
                }

                //We reached the max call, we have to wait till _resetDate
                if (currentCallNumber == maxCallNumber)
                {
                    _reached[i] = true;
                }
            }
        }

        public bool IsLimited(out int nbSeconds)
        {
            DateTime current = DateTime.Now;
            DateTime next = DateTime.Now;
            bool reached = false;

            for (var i = 0; i < _reached.Length; i++)
            {
                if (_reached[i] && _resetDate[i] > next)
                {
                    next = _resetDate[i];
                    reached = true;
                }
            }

            if (reached)
            {
                nbSeconds = (int)Math.Ceiling((next - current).TotalSeconds);
                return true;
            }

            nbSeconds = 0;
            return false;
        }
    }
}