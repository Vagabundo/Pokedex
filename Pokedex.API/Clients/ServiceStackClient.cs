using Pokedex.API.Data;
using ServiceStack;
using ServiceStack.Text;

namespace Pokedex.API.Clients
{
    public class ServiceStackClient : IPokemonClient
    {
        private string url = "https://pokeapi.co/api/v2/pokemon-species/";
        private string userAgent = "VagaPokedexApi/1.0";
        public Pokemon GetInfo(int id)
        {
            var json = JsonObject.Parse(
                (url+id)
                    .GetJsonFromUrl(webReq =>
                    {
                        webReq.UserAgent = userAgent;
                    }));

            return new Pokemon {
                Name = GetPokemonName(json),
                Description = GetPokemonDescription(json),
                Habitat = GetPokemonHabitat(json),
                IsLegendary = GetPokemonIsLegendary(json)
            };
        }

        private string GetPokemonName(JsonObject json)
        {
            return "";
        }

        private string GetPokemonDescription(JsonObject json)
        {
            return "";
        }

        private string GetPokemonHabitat(JsonObject json)
        {
            return "";
        }
        private bool GetPokemonIsLegendary(JsonObject json)
        {
            return false;
        }
    }
}