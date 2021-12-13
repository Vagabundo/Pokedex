using Pokedex.API.Data;

namespace Pokedex.API.Test.Data
{
    public static class Constants
    {
        public static int PokemonId = 1;

        public static Pokemon FakePokemon = new Pokemon {
            Name = "testemon",
            Description = "This is a test pokemon description.",
            Habitat = "testiland",
            IsLegendary = true
        };

        public static string FakePokemonJson = 
        "{\"flavor_text_entries\":[{\"flavor_text\":\"This\\nis\\ra\\ftest\\tpokemon\\bdescription.\","+
        "\"language\":{\"name\":\"en\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/language\\/9\\/\"},\"version\":{\"name\":\"red\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/version\\/1\\/\"}}],"+
        "\"habitat\":{\"name\":\"" + FakePokemon.Habitat + "\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/pokemon-habitat\\/5\\/\"},\"id\":" + PokemonId + ",\"is_baby\":false,"+
        "\"is_legendary\":" + FakePokemon.IsLegendary.ToString().ToLower() + ",\"is_mythical\":false,\"name\":\"" + FakePokemon.Name + "\"}";

        public static string FakeTranslationJson = 
        "{\"success\":{\"total\":1},\"contents\":{\"translated\":\"A test pokemon description,  this is.\",\"text\":\"This is a test pokemon description.\",\"translation\":\"yoda\"}}";

        public static string FakePokemonFromNameJson = "{\"height\": 16, \"id\": " + PokemonId + "}";

        public static string TranslationErrorExample = "{ \"error\": { \"code\": 429, \"message\": \"Too Many Requests: Rate limit of 5 requests per hour exceeded. Please wait for 44 minutes and 14 seconds.\" } }";
        
        public static string FakeTranslation = "A test pokemon description, this is.";

        public static string FakeFailedTranslationResponse = "{\n    \"error\": {\n        \"code\": 429,\n        \"message\": \"Too Many Requests: Rate limit of 5 requests per hour exceeded. Please wait for 56 minutes and 56 seconds.\"\n    }\n}";

        public static string InternalErrorMessage = "Internal Error";
    }
}