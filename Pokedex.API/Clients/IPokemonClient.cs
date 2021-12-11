using Pokedex.API.Data;

namespace Pokedex.API.Clients
{
    public interface IPokemonClient
    {
        Pokemon GetInfo(int id);
    }
}