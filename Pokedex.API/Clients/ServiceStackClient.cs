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

        public Task<string> GetPokemonInfoFromNameAsync(string name)
        {
            return GetJsonFromUrlAsync(_clientConfig.PokeApiBaseUrl + _clientConfig.PokeApiNameUrl + name);
        }

        public Task<string> GetPokemonInfoFromIdAsync(int id)
        {
            return GetJsonFromUrlAsync(_clientConfig.PokeApiBaseUrl + _clientConfig.PokeApiIdUrl + id);
        }

        public Task<string> GetTranslatedTextAsync(string text, bool yoda)
        {
            var url = _clientConfig.TranslationsApiUrl.Base +
                    (yoda ? _clientConfig.TranslationsApiUrl.Yoda :
                    _clientConfig.TranslationsApiUrl.Shakespeare) + 
                    _clientConfig.TranslationsApiUrl.End;

            return GetJsonFromUrlAsync(url + text);
        }

        private Task<string> GetJsonFromUrlAsync(string url)
        {
            return url.GetJsonFromUrlAsync(webReq =>
                {
                    webReq.UserAgent = _clientConfig.UserAgent;
                });
        }
    }
}