using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pokedex.API.Data;
using Pokedex.API.Managers;

namespace Pokedex.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokedexController : ControllerBase
    {
        private PokedexManager _manager;

        public PokedexController(PokedexManager manager)
        {
            _manager = manager;
        }

        [HttpGet("{name}")]
        public Task<Pokemon> GetPokemon(string name)
        {
            return _manager.GetPokemonFromNameAsync(name);
        }

        [HttpGet("translated/{name}")]
        public Task<Pokemon> GetTranslatedPokemon(string name)
        {
            return _manager.GetTranslatedPokemonFromNameAsync(name);
        }
    }
}