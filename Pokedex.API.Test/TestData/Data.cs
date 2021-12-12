using Pokedex.API.Data;

namespace Pokedex.API.Test.Data
{
    public static class Constants
    {
        public static string ExpectedJson = 
        "{\"base_happiness\":0,\"capture_rate\":3,\"color\":{\"name\":\"purple\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/pokemon-color\\/7\\/\"},"+
        "\"egg_groups\":[{\"name\":\"no-eggs\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/egg-group\\/15\\/\"}],\"evolution_chain\":{\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/evolution-chain\\/77\\/\"},"+
        "\"evolves_from_species\":null,\"flavor_text_entries\":[{\"flavor_text\":\"It was created by\\na scientist after\\nyears of horrific\\fgene splicing and\\nDNA engineering\\nexperiments.\","+
        "\"language\":{\"name\":\"en\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/language\\/9\\/\"},\"version\":{\"name\":\"red\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/version\\/1\\/\"}},"+
        "{\"flavor_text\":\"It was created by\\na scientist after\\nyears of horrific\\fgene splicing and\\nDNA engineering\\nexperiments.\","+
        "\"language\":{\"name\":\"en\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/language\\/9\\/\"},\"version\":{\"name\":\"blue\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/version\\/2\\/\"}}],"+
        "\"habitat\":{\"name\":\"rare\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/pokemon-habitat\\/5\\/\"},\"has_gender_differences\":false,\"hatch_counter\":120,\"id\":150,\"is_baby\":false,"+
        "\"is_legendary\":true,\"is_mythical\":false,\"name\":\"mewtwo\"}";

        public static string FakeExpectedJson = 
        "{\"flavor_text_entries\":[{\"flavor_text\":\"This\\nis\\ra\\ftest\\tpokemon\\bdescription.\","+
        "\"language\":{\"name\":\"en\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/language\\/9\\/\"},\"version\":{\"name\":\"red\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/version\\/1\\/\"}}],"+
        "\"habitat\":{\"name\":\"testiland\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/pokemon-habitat\\/5\\/\"},\"id\":150,\"is_baby\":false,"+
        "\"is_legendary\":true,\"is_mythical\":false,\"name\":\"testemon\"}";

        public static Pokemon FakePokemon = new Pokemon {
                Name = "testemon",
                Description = "This is a test pokemon description.",
                Habitat = "testiland",
                IsLegendary = true
            };
    }
}