using System.Threading.Tasks;

namespace Pokedex.API.Clients
{
    public interface IPokemonClient
    {
        Task<string> GetInfoAsync(int id);
        Task<string> GetTranslatedTextAsync(string text);
    }
}