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

        [HttpGet("{id}")]
        public Task<Pokemon> GetPokemon(int id)
        {
            return _manager.GetPokemonFromIdAsync(id);
        }
    }
}