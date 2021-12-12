using Pokedex.API.Data;

namespace Pokedex.API.Test.Data
{
    public static class Constants
    {
        public static string FakePokemonJson = 
        "{\"flavor_text_entries\":[{\"flavor_text\":\"This\\nis\\ra\\ftest\\tpokemon\\bdescription.\","+
        "\"language\":{\"name\":\"en\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/language\\/9\\/\"},\"version\":{\"name\":\"red\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/version\\/1\\/\"}}],"+
        "\"habitat\":{\"name\":\"testiland\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/pokemon-habitat\\/5\\/\"},\"id\":150,\"is_baby\":false,"+
        "\"is_legendary\":true,\"is_mythical\":false,\"name\":\"testemon\"}";

        public static string FakeTranslationJson = 
        "{\"success\":{\"total\":1},\"contents\":{\"translated\":\"A test pokemon description, this is.\",\"text\":\"This is a test pokemon description.\",\"translation\":\"yoda\"}}";

        public static Pokemon FakePokemon = new Pokemon {
                Name = "testemon",
                Description = "This is a test pokemon description.",
                Habitat = "testiland",
                IsLegendary = true
            };
        
        public static string FakeTranslation = "A test pokemon description, this is.";
    }
}