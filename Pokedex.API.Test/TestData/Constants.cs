using Pokedex.API.Data;

namespace Pokedex.API.Test.Data
{
    public static class Constants
    {
        public static ClientDataConfig FakeClientConfig = new ClientDataConfig
        {
            PokeApiBaseUrl = "http://test.test/",
            PokeApiIdUrl = "test/",
            PokeApiNameUrl = "test/",
            TranslationsApiUrl = new TranslationsApiUrl
            {
                Base = "http://test.test/",
                Yoda = "test/",
                Shakespeare = "test/",
                End = "test"
            },
            UserAgent = "test-agent"
        };

        public static int PokemonId = 1;

        public static Pokemon FakePokemon = Utils.PokemonBuilder();

        public static string FakePokemonJson = Utils.PokemonJsonBuilder(FakePokemon, 1);

        public static string FakeTranslationJson = 
        "{\"success\":{\"total\":1},\"contents\":{\"translated\":\"A test pokemon description,  this is.\",\"text\":\"This is a test pokemon description.\",\"translation\":\"yoda\"}}";

        public static string FakePokemonFromNameJson = "{\"height\": 16, \"id\": " + PokemonId + "}";

        public static string TranslationErrorExample = "{ \"error\": { \"code\": 429, \"message\": \"Too Many Requests: Rate limit of 5 requests per hour exceeded. Please wait for 44 minutes and 14 seconds.\" } }";
        
        public static string FakeTranslation = "A test pokemon description, this is.";

        public static string FakeFailedTranslationResponse = "{\n    \"error\": {\n        \"code\": 429,\n        \"message\": \"Too Many Requests: Rate limit of 5 requests per hour exceeded. Please wait for 56 minutes and 56 seconds.\"\n    }\n}";

        public static string InternalErrorMessage = "Internal Error";
    }
}