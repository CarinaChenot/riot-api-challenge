using System;
using System.Net.Http;
using System.Threading.Tasks;
using Jil;
using LolAPI.DTO;
using log4net;

namespace LolAPI
{
    internal class ApiHttpClient
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(ApiHttpClient));
        private readonly HttpClient _client;
        private readonly string _key;
        private readonly bool _throwException;


        public ApiHttpClient(string key, bool throwException = false)
        {
            _client = new HttpClient(new RateLimitingHandler()) { Timeout = TimeSpan.FromSeconds(200) };
            _key = key;
            _throwException = throwException;
        }

        public async Task<T> Get<T>(string url, TimeSpan? cacheDuration = null, string method = null)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://euw1.api.riotgames.com" + url);
                request.Headers.Add("X-Riot-Token", _key);

                method = url;
                if (method != null) request.Properties.Add("Method", method);

                var response = await _client.SendAsync(request);

                if ((int)response.StatusCode == 429)
                {
                    int delay = int.Parse(await response.Content.ReadAsStringAsync());
                    _logger.InfoFormat("Waiting for {0} seconds before another call", delay);

                    await Task.Delay(1000 * delay);

                    return await Get<T>(url, cacheDuration, method);
                }

                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JSON.Deserialize<T>(content);
                }

                var erreur = JSON.Deserialize<ErreurDTO>(content);
              
                if (_throwException)
                    throw new Exception(erreur.status.message);
                else
                    _logger.WarnFormat("Erreur lors de l'appel à {0} : {1}", url, erreur.status.message);

                return default(T);
            }
            catch (Exception e)
            {
                _logger.WarnFormat("Erreur lors de l'appel à {0} : {1}", url, e);
                throw;
            }
        }
    }
}
