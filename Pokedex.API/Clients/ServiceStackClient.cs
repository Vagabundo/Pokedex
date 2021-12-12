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

        public ServiceStackClient(IConfiguration config)
        {
            _config = config;
        }

        public Task<string> GetInfoAsync(int id)
        {
            var url = _config.GetValue<string>("PokeApiUrl");
            return (url+id).GetJsonFromUrlAsync(webReq =>
                {
                    webReq.UserAgent = _config.GetValue<string>("UserAgent");
                });
        }

        public Task<string> GetTranslatedTextAsync(string text)
        {
            throw new System.NotImplementedException();
        }
    }
}