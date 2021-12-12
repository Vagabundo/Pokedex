using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Pokedex.API.Data;
using ServiceStack;

namespace Pokedex.API.Clients
{
    public class ServiceStackClient : IPokemonClient
    {
        private readonly IConfiguration _config;
        private readonly string apiBaseUrl;

        public ServiceStackClient(IConfiguration config)
        {
            _config = config;
            apiBaseUrl = _config.GetValue<string>("PokeApiBaseUrl");
        }

        public Task<string> GetPokemonInfoFromNameAsync(string name)
        {
            var url = apiBaseUrl + _config.GetValue<string>("PokeApiNameUrl");
            return (url+name).GetJsonFromUrlAsync(webReq =>
                {
                    webReq.UserAgent = _config.GetValue<string>("UserAgent");
                });
        }

        public Task<string> GetPokemonInfoFromIdAsync(int id)
        {
            var url = apiBaseUrl + _config.GetValue<string>("PokeApiIdUrl");
            return (url+id).GetJsonFromUrlAsync(webReq =>
                {
                    webReq.UserAgent = _config.GetValue<string>("UserAgent");
                });
        }

        public Task<string> GetTranslatedTextAsync(string text)
        {
            var url = _config.GetValue<string>("YodaTranslationApiUrl");
            return (url+text).GetJsonFromUrlAsync(webReq =>
                {
                    webReq.UserAgent = _config.GetValue<string>("UserAgent");
                });
        }
    }
}