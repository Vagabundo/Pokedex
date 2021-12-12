using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Pokedex.API.Clients;
using Pokedex.API.Data;
using ServiceStack;

namespace Pokedex.API.Managers
{
    public class PokedexManager {
        private IPokemonClient _client;
        public PokedexManager(IPokemonClient pokemonClient)
        {
            _client = pokemonClient;
        }

        public async Task<Pokemon> GetPokemonFromIdAsync(int id)
        {
            var response = await _client.GetInfoAsync(id);
            dynamic json =  DynamicJson.Deserialize(response);

            return new Pokemon {
                Name = GetPokemonName(json),
                Description = GetPokemonDescription(json),
                Habitat = GetPokemonHabitat(json),
                IsLegendary = GetPokemonIsLegendary(json)
            };
        }

        public async Task<Pokemon> GetTranslatedPokemonFromIdAsync(int id)
        {
            Pokemon poke = await GetPokemonFromIdAsync(id);
            poke.Description = await _client.GetTranslatedTextAsync(poke.Description);

            return poke;
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