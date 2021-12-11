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
        public Pokemon GetPokemon(int id)
        {
            return _manager.GetPokemonFromId(id);
        }
    }
}