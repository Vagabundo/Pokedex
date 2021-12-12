using System.Text.RegularExpressions;
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

        public Pokemon GetInfo(int id)
        {
            var url = _config.GetValue<string>("PokeApiUrl");
            var res = (url+id).GetJsonFromUrlAsync(webReq =>
                    {
                        webReq.UserAgent = _config.GetValue<string>("UserAgent");
                    });
            
            var json = DynamicJson.Deserialize(res.Result);

            return new Pokemon {
                Name = GetPokemonName(json),
                Description = GetPokemonDescription(json),
                Habitat = GetPokemonHabitat(json),
                IsLegendary = GetPokemonIsLegendary(json)
            };
        }

        private string GetPokemonName(dynamic json)
        {
            return json.name;
        }

        private string GetPokemonDescription(dynamic json)
        {
            return Regex.Replace(json.flavor_text_entries[0].flavor_text, "\n|\r|\f|\b|\t", " ");
        }

        private string GetPokemonHabitat(dynamic json)
        {
            return json.habitat.name;
        }

        private bool GetPokemonIsLegendary(dynamic json)
        {
            return json.is_legendary == "true";
        }

        public string GetTranslatedText(string text)
        {
            throw new System.NotImplementedException();
        }
    }
}