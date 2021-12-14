using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pokedex.API.Clients;
using Pokedex.API.Data;
using ServiceStack;

namespace Pokedex.API.Managers
{
    public class PokemonManager {
        private IPokemonClient _client;
        private readonly ILogger<PokemonManager> _logger;

        public PokemonManager(IPokemonClient pokemonClient, ILogger<PokemonManager> logger)
        {
            _client = pokemonClient;
            _logger = logger;
        }

        /// <summary>
        /// Returns a specific pokemon information by name
        /// </summary>
        /// <param name="name">Pokemon name</param>
        public async Task<Response<Pokemon>> GetPokemonFromNameAsync(string name)
        {
            try
            {
                string response = await _client.GetPokemonInfoFromNameAsync(name);  
                return await GetPokemonFromIdAsync(JsonHelper.GetPokemonId(response));
            }
            catch (Exception ex) 
            {
                if (ex.GetStatus() != null)
                    return new Response<Pokemon>(((int)ex.GetStatus()), ex.GetResponseBody());

                _logger.Log(LogLevel.Error, ex.Message);
                return new Response<Pokemon>(500, "Internal Error");
            }
        }

        /// <summary>
        /// Returns a specific pokemon information by id
        /// </summary>
        /// <param name="id">Pokemon id</param>
        public async Task<Response<Pokemon>> GetPokemonFromIdAsync(int id)
        {
            try 
            {
                string response = await _client.GetPokemonInfoFromIdAsync(id);
                
                return new Response<Pokemon>(new Pokemon 
                {
                    Name = JsonHelper.GetPokemonName(response),
                    Description = JsonHelper.GetPokemonDescription(response),
                    Habitat = JsonHelper.GetPokemonHabitat(response),
                    IsLegendary = JsonHelper.GetPokemonIsLegendary(response)
                });
            }
            catch (Exception ex) 
            {
                if (ex.GetStatus() != null)
                    return new Response<Pokemon>(((int)ex.GetStatus()), ex.GetResponseBody());

                _logger.Log(LogLevel.Error, ex.Message);
                return new Response<Pokemon>(500, "Internal Error");
            }
        }

        /// <summary>
        /// Returns a specific pokemon information with tranbspalet description by pokemon name
        /// </summary>
        /// <param name="name">Pokemon name</param>
        public async Task<Response<Pokemon>> GetTranslatedPokemonFromNameAsync(string name)
        {
            Response<Pokemon> pokeResponse;
            Task<string> response;

            pokeResponse = await GetPokemonFromNameAsync(name);
            if(pokeResponse.Body != null)
            {
                try 
                {      
                    response = _client.GetTranslatedTextAsync(pokeResponse.Body.Description, YodaTranslation(pokeResponse.Body));
                    pokeResponse.Body.Description = JsonHelper.GetPokemonTranslatedDescription(await response);
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Warning, ex.Message);
                    return pokeResponse;
                }
            }

            return pokeResponse;
        }

        /// <summary>
        /// Returns whether a pokemon description should be translated using yoda translator
        /// </summary>
        /// <param name="poke">Pokemon</param>
        private bool YodaTranslation(Pokemon poke)
        {
            return poke.IsLegendary || poke.Habitat.Equals("cave");
        }
    }
}