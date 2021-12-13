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

        public async Task<Response<Pokemon>> GetPokemonFromNameAsync(string name)
        {
            string response = await _client.GetPokemonInfoFromNameAsync(name);

            return await GetPokemonFromIdAsync(JsonHelper.GetPokemonId(response));
        }

        public async Task<Response<Pokemon>> GetPokemonFromIdAsync(int id)
        {
            string response = await _client.GetPokemonInfoFromIdAsync(id);

            return new Response<Pokemon>(new Pokemon {
                Name = JsonHelper.GetPokemonName(response),
                Description = JsonHelper.GetPokemonDescription(response),
                Habitat = JsonHelper.GetPokemonHabitat(response),
                IsLegendary = JsonHelper.GetPokemonIsLegendary(response)
            });
        }

        public async Task<Response<Pokemon>> GetTranslatedPokemonFromNameAsync(string name)
        {
            Response<Pokemon> poke = await GetPokemonFromNameAsync(name);
            if(poke.Body == null)
            {
                return poke;
            }

            string response = await _client.GetTranslatedTextAsync(poke.Body.Description);
            poke.Body.Description = JsonHelper.GetPokemonTranslatedDescription(response);

            return poke;
        }
    }
}