using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Pokedex.API.Clients;
using Pokedex.API.Data;
using ServiceStack;

namespace Pokedex.API.Managers
{
    public class PokedexManager {
        private IPokemonClient _client;
        public PokedexManager(IPokemonClient pokemonClient)
        {
            _client = pokemonClient;
        }

        public async Task<Pokemon> GetPokemonFromNameAsync(string name)
        {
            string response = await _client.GetPokemonInfoFromNameAsync(name);
            dynamic json =  DynamicJson.Deserialize(response);
            int id = 0;

            return Int32.TryParse(json?.id, out id) ? await GetPokemonFromIdAsync(id) : null;
        }

        public async Task<Pokemon> GetPokemonFromIdAsync(int id)
        {
            string response = await _client.GetPokemonInfoFromIdAsync(id);
            dynamic json =  DynamicJson.Deserialize(response);

            return new Pokemon {
                Name = GetPokemonName(json),
                Description = GetPokemonDescription(json),
                Habitat = GetPokemonHabitat(json),
                IsLegendary = GetPokemonIsLegendary(json)
            };
        }

        public async Task<Pokemon> GetTranslatedPokemonFromIdAsync(int id)
        {
            Pokemon poke = await GetPokemonFromIdAsync(id);
            string response = await _client.GetTranslatedTextAsync(poke.Description);
            dynamic json = DynamicJson.Deserialize(response);

            poke.Description = GetPokemonTranslatedDescription(json);

            return poke;
        }

        // In production, the following functions would have exceptions handling depending on the requirements
        private string GetPokemonName(dynamic json)
        {
            return json?.name;
        }

        private string GetPokemonDescription(dynamic json)
        {
            return Regex.Replace(json?.flavor_text_entries[0]?.flavor_text, "\n|\r|\f|\b|\t", " ");
        }

        private string GetPokemonTranslatedDescription(dynamic json)
        {
            // The responses from translations API sometimes include double spaces after comma in the strings, so I have to filter it 
            return json?.success?.total == "0" ? null : Regex.Replace(json?.contents?.translated, "  ", " ");
        }

        private string GetPokemonHabitat(dynamic json)
        {
            return json?.habitat?.name;
        }

        private bool GetPokemonIsLegendary(dynamic json)
        {
            return json?.is_legendary == "true";
        }
    }
}