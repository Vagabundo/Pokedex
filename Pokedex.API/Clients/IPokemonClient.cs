using System.Threading.Tasks;

namespace Pokedex.API.Clients
{
    public interface IPokemonClient
    {
        /// <summary>
        /// Returns a specific pokemon information in json by name
        /// </summary>
        /// <param name="name">Pokemon name</param>
        Task<string> GetPokemonInfoFromNameAsync(string name);

        /// <summary>
        /// Returns a specific pokemon information in json by id
        /// </summary>
        /// <param name="name">Pokemon name</param>
        Task<string> GetPokemonInfoFromIdAsync(int id);

        /// <summary>
        /// Returns the translation of a specific text using Yoda or Shakespeare
        /// </summary>
        /// <param name="text">Text to be translated</param>
        /// <param name="yoda"> Whether use Yoda translator or not</param>
        Task<string> GetTranslatedTextAsync(string text, bool yoda);
    }
}