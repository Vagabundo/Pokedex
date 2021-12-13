using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pokedex.API.Data;
using Pokedex.API.Managers;

namespace Pokedex.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private PokemonManager _manager;

        public PokemonController(PokemonManager manager)
        {
            _manager = manager;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Pokemon>> GetPokemon(string name)
        {
            var response = await _manager.GetPokemonFromNameAsync(name);

            if (response.ErrorMessage == null)
                return Ok(response.Body);

            return StatusCode(response.ErrorCode, response.ErrorMessage);
        }

        [HttpGet("translated/{name}")]
        public async Task<ActionResult<Pokemon>> GetTranslatedPokemon(string name)
        {
            var response = await _manager.GetTranslatedPokemonFromNameAsync(name);

            if (response.ErrorMessage == null)
                return Ok(response.Body);

            return StatusCode(response.ErrorCode, response.ErrorMessage);
        }
    }
}