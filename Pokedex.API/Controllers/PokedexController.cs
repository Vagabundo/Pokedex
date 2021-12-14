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

        /// <summary>
        /// Returns a specific pokemon information by name
        /// </summary>
        /// <response code="200">Pokemon information returned</response>
        /// <response code="404">Pokemon not found</response>
        /// <response code="500">Can't return Pokemon information right now</response>
        [ProducesResponseType(typeof(Pokemon), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(500)]
        [HttpGet("{name}")]
        public async Task<ActionResult<Pokemon>> GetPokemon(string name)
        {
            var response = await _manager.GetPokemonFromNameAsync(name);

            if (response.ErrorMessage == null)
                return Ok(response.Body);

            return StatusCode(response.ErrorCode, response.ErrorMessage);
        }

        /// <summary>
        /// Returns a specific pokemon information by name with translated description
        /// </summary>
        /// <response code="200">Pokemon information returned</response>
        /// <response code="404">Pokemon not found</response>
        /// <response code="500">Can't return Pokemon information right now</response>
        [ProducesResponseType(typeof(Pokemon), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(500)]
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