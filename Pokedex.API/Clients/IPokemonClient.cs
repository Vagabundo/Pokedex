using System.Threading.Tasks;

namespace Pokedex.API.Clients
{
    public interface IPokemonClient
    {
        Task<string> GetPokemonInfoFromNameAsync(string name);
        Task<string> GetPokemonInfoFromIdAsync(int id);
        Task<string> GetTranslatedTextAsync(string text);
    }
}