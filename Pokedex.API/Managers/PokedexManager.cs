using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pokedex.API.Clients;
using Pokedex.API.Data;
using ServiceStack;

namespace Pokedex.API.Managers
{
    public class PokedexManager {
        private IPokemonClient _client;
        private readonly ILogger<PokedexManager> _logger;

        public PokedexManager(IPokemonClient pokemonClient, ILogger<PokedexManager> logger)
        {
            _client = pokemonClient;
            _logger = logger;
        }

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

        public async Task<Response<Pokemon>> GetPokemonFromIdAsync(int id)
        {
            try 
            {
                string response = await _client.GetPokemonInfoFromIdAsync(id);
                
                return new Response<Pokemon>(new Pokemon {
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

        public async Task<Response<Pokemon>> GetTranslatedPokemonFromNameAsync(string name)
        {
            try
            {
                Response<Pokemon> poke = await GetPokemonFromNameAsync(name);
                if(poke.Body == null)
                {
                    return poke;
                }

                string response = await _client.GetTranslatedTextAsync(poke.Body.Description);
                poke.Body.Description = JsonHelper.GetPokemonTranslatedDescription(response);

                return poke;
            }
            catch (Exception ex) 
            {
                if (ex.GetStatus() != null)
                    return new Response<Pokemon>(((int)ex.GetStatus()), ex.GetResponseBody());

                _logger.Log(LogLevel.Error, ex.Message);
                return new Response<Pokemon>(500, "Internal Error");
            }
        }
    }
}