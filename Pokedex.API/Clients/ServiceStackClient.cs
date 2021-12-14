using System.Threading.Tasks;
using Pokedex.API.Data;
using ServiceStack;

namespace Pokedex.API.Clients
{
    public class ServiceStackClient : IPokemonClient
    {
        private readonly ClientDataConfig _clientConfig;

        public ServiceStackClient(ClientDataConfig clientDataConfig)
        {
            _clientConfig = clientDataConfig;
        }

        /// <inheritdoc />
        public Task<string> GetPokemonInfoFromNameAsync(string name)
        {
            return GetJsonFromUrlAsync(_clientConfig.PokeApiBaseUrl + _clientConfig.PokeApiNameUrl + name);
        }

        /// <inheritdoc />
        public Task<string> GetPokemonInfoFromIdAsync(int id)
        {
            return GetJsonFromUrlAsync(_clientConfig.PokeApiBaseUrl + _clientConfig.PokeApiIdUrl + id);
        }

        /// <inheritdoc />
        public Task<string> GetTranslatedTextAsync(string text, bool yoda)
        {
            var url = _clientConfig.TranslationsApiUrl.Base +
                    (yoda ? _clientConfig.TranslationsApiUrl.Yoda :
                    _clientConfig.TranslationsApiUrl.Shakespeare) + 
                    _clientConfig.TranslationsApiUrl.End;

            return GetJsonFromUrlAsync(url + text);
        }

        /// <summary>
        /// Returns the response as json from the given url
        /// </summary>
        /// <param name="url">url</param>
        private Task<string> GetJsonFromUrlAsync(string url)
        {
            return url.GetJsonFromUrlAsync(webReq =>
                {
                    webReq.UserAgent = _clientConfig.UserAgent;
                });
        }
    }
}