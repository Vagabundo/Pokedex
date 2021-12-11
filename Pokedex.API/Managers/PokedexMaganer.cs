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

        public Pokemon GetPokemonFromId(int id)
        {
            return _client.GetInfo(id);
        }
    }
}