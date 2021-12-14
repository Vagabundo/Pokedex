using Pokedex.API.Data;

namespace Pokedex.API.Test.Data
{
    public static class Utils
    {
        public static string PokemonJsonBuilder (Pokemon pokemon, int id)
        {
            return "{\"flavor_text_entries\":[{\"flavor_text\":\"This\\nis\\ra\\ftest\\tpokemon\\bdescription.\","+
            "\"language\":{\"name\":\"en\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/language\\/9\\/\"},\"version\":{\"name\":\"red\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/version\\/1\\/\"}}],"+
            "\"habitat\":{\"name\":\"" + pokemon.Habitat + "\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/pokemon-habitat\\/5\\/\"},\"id\":" + id + ",\"is_baby\":false,"+
            "\"is_legendary\":" + pokemon.IsLegendary.ToString().ToLower() + ",\"is_mythical\":false,\"name\":\"" + pokemon.Name + "\"}";
        }

        public static Pokemon PokemonBuilder (
            string name = "testemon",
            string description = "This is a test pokemon description.",
            string habitat = "testiland",
            bool isLegendary = false)
        {
            return new Pokemon
            {
                Name = name,
                Description = description,
                Habitat = habitat,
                IsLegendary = isLegendary
            };
        }
    }
}