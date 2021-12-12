using System.Threading.Tasks;
using Pokedex.API.Clients;
using Pokedex.API.Data;

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
            string response = await _client.GetPokemonInfoFromNameAsync(name);

            return await GetPokemonFromIdAsync(JsonHelper.GetPokemonId(response));
        }

        public async Task<Pokemon> GetPokemonFromIdAsync(int id)
        {
            string response = await _client.GetPokemonInfoFromIdAsync(id);

            return new Pokemon {
                Name = JsonHelper.GetPokemonName(response),
                Description = JsonHelper.GetPokemonDescription(response),
                Habitat = JsonHelper.GetPokemonHabitat(response),
                IsLegendary = JsonHelper.GetPokemonIsLegendary(response)
            };
        }

        public async Task<Pokemon> GetTranslatedPokemonFromNameAsync(string name)
        {
            Pokemon poke = await GetPokemonFromNameAsync(name);
            string response = await _client.GetTranslatedTextAsync(poke.Description);

            poke.Description = JsonHelper.GetPokemonTranslatedDescription(response);

            return poke;
        }
    }
}