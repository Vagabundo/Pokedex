using System.Text.RegularExpressions;
using Pokedex.API.Data;
using ServiceStack;

namespace Pokedex.API.Clients
{
    public class ServiceStackClient : IPokemonClient
    {
        private string url = "https://pokeapi.co/api/v2/pokemon-species/";
        private string userAgent = "VagaPokedexApi/1.0";
        public Pokemon GetInfo(int id)
        {
            var res = (url+id).GetJsonFromUrlAsync(webReq =>
                    {
                        webReq.UserAgent = userAgent;
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
    }
}