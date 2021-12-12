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

        public async Task<Pokemon> GetPokemonFromNameAsync(string name)
        {
            return await Task.FromResult(new Pokemon {
                Name = "Failemon",
                Description = "Dummy pokemon for a failing test",
                Habitat = "Failand",
                IsLegendary = false
            });
        }

        public async Task<Pokemon> GetPokemonFromIdAsync(int id)
        {
            string response = await _client.GetPokemonInfoFromIdAsync(id);
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
            string response = await _client.GetTranslatedTextAsync(poke.Description);
            dynamic json = DynamicJson.Deserialize(response);
            poke.Description = GetPokemonTranslatedDescription(json);

            return poke;
        }

        private async Task<int> GetPokemonIdFromNameAsync(string name)
        {
            return await Task.FromResult(1);
        }

        // In production, the following functions would have exceptions handling depending on the requirements
        private string GetPokemonName(dynamic json)
        {
            return json?.name;
        }

        private string GetPokemonDescription(dynamic json)
        {
            return Regex.Replace(json?.flavor_text_entries[0]?.flavor_text, "\n|\r|\f|\b|\t", " ");
        }

        private string GetPokemonTranslatedDescription(dynamic json)
        {
            return json?.success?.total == "0" ? null : json?.contents?.translated;
        }

        private string GetPokemonHabitat(dynamic json)
        {
            return json?.habitat?.name;
        }

        private bool GetPokemonIsLegendary(dynamic json)
        {
            return json?.is_legendary == "true";
        }
    }
}