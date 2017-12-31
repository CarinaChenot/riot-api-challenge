using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace LolAPI
{
    internal class RateLimitingHandler : DelegatingHandler
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(ApiHttpClient));
        private readonly RateLimiter _rateLimiter = new RateLimiter();
        private readonly Dictionary<string, DateTime> _serviceLimiter = new Dictionary<string, DateTime>();

        public RateLimitingHandler() :
            base(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //Get rate limiter for the application
            RateLimiter appRateLimiter = _rateLimiter.Get("App");

            //Get rate limiter for the method
            RateLimiter methodRateLimiter = _rateLimiter.Get("METHOD_" + request.Properties["Method"]);

            //Get limit for service
            DateTime serviceLimiter = _serviceLimiter.ContainsKey("EUW1") ? _serviceLimiter["EUW1"] : DateTime.MinValue;

            int nbSeconds;
            int secondsToWait = 0;

            if (appRateLimiter.IsLimited(out nbSeconds) && nbSeconds > 0)
            {
                secondsToWait = Math.Max(secondsToWait, nbSeconds);
                //_logger.Debug("App limited for " + nbSeconds);
            }

            if (methodRateLimiter.IsLimited(out nbSeconds) && nbSeconds > 0)
            {
                secondsToWait = Math.Max(secondsToWait, nbSeconds);
                //_logger.Debug("Method limited for " + nbSeconds);
            }

            if (serviceLimiter > DateTime.Now)
            {
                secondsToWait = Math.Max(secondsToWait, nbSeconds);
                //_logger.Debug("Service limited for " + nbSeconds);
            }

            if (secondsToWait > 0)
            {
                return new HttpResponseMessage((HttpStatusCode)429) { Content = new StringContent(secondsToWait.ToString()) };
            }

            //We want the response to be gziped so we save bandwith
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            var key = request.Headers.GetValues("X-Riot-Token").First();
            _logger.InfoFormat("[KEY {1}] Query {0}", request.RequestUri, key.Substring(key.Length - 5));


            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            HttpResponseHeaders headers = response.Headers;

            //Read limits values
            if (headers.Contains("X-App-Rate-Limit"))
            {
                appRateLimiter.Read(headers.GetValues("X-App-Rate-Limit").First(), headers.GetValues("X-App-Rate-Limit-Count").First());
            }

            if (headers.Contains("X-Method-Rate-Limit"))
            {
                methodRateLimiter.Read(headers.GetValues("X-Method-Rate-Limit").First(), headers.GetValues("X-Method-Rate-Limit-Count").First());
            }

            //still limited
            if ((int)response.StatusCode == 429)
            {
                _logger.Info("Aie");

                string rateLimitType = response.Headers.GetValues("X-Rate-Limit-Type").FirstOrDefault();
                string retryAfter = response.Headers.GetValues("Retry-After").FirstOrDefault();

                //underlying service rate limited
                if (retryAfter == null)
                {
                    _logger.Info("Underlying service limited");
                    await Task.Delay(1000, cancellationToken);
                    return await SendAsync(request, cancellationToken);
                }


                //riot API service limited
                int retryAfterNbSeconds = Math.Max(0, int.Parse(retryAfter));
                _logger.Info("Service limited for " + retryAfter);

                _serviceLimiter["EUW1"] = DateTime.Now.AddSeconds(retryAfterNbSeconds);

                return new HttpResponseMessage((HttpStatusCode)429) { Content = new StringContent(retryAfterNbSeconds.ToString()) };
            }

            return response;
        }
    }
}