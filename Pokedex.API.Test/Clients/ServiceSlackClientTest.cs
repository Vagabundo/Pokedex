using Moq;
using NUnit.Framework;
using Pokedex.API.Data;
using Pokedex.API.Clients;
using ServiceStack;

namespace Pokedex.API.Test.Clients
{
    [TestFixture]
    public class ServiceStackClientTest
    {
        private ServiceStackClient client;
        private const string json =
        "{\"base_happiness\":0,\"capture_rate\":3,\"color\":{\"name\":\"purple\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/pokemon-color\\/7\\/\"},"+
        "\"egg_groups\":[{\"name\":\"no-eggs\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/egg-group\\/15\\/\"}],\"evolution_chain\":{\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/evolution-chain\\/77\\/\"},"+
        "\"evolves_from_species\":null,\"flavor_text_entries\":[{\"flavor_text\":\"It was created by\\na scientist after\\nyears of horrific\\fgene splicing and\\nDNA engineering\\nexperiments.\","+
        "\"language\":{\"name\":\"en\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/language\\/9\\/\"},\"version\":{\"name\":\"red\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/version\\/1\\/\"}},"+
        "{\"flavor_text\":\"It was created by\\na scientist after\\nyears of horrific\\fgene splicing and\\nDNA engineering\\nexperiments.\","+
        "\"language\":{\"name\":\"en\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/language\\/9\\/\"},\"version\":{\"name\":\"blue\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/version\\/2\\/\"}}],"+
        "{\"habitat\":{\"name\":\"rare\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/pokemon-habitat\\/5\\/\"},\"has_gender_differences\":false,\"hatch_counter\":120,\"id\":150,\"is_baby\":false,"+
        "\"is_legendary\":true,\"is_mythical\":false,\"name\":\"mewtwo\"}}";
        private const string exampleUrl = "http://test.test";

        [SetUp]
        public void Setup()
        {
            client = new ServiceStackClient();
        }

        [Test]
        public void GetInfo_ReturnsExpectedPokemonObject()
        {
            var mewtwo = new Pokemon {
                Name = "mewtwo",
                Description = "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.",
                Habitat = "rare",
                IsLegendary = true
            };

            using (new HttpResultsFilter{ StringResult = json })
            {
                Assert.That(exampleUrl.GetJsonFromUrl(), Is.EqualTo(json));
                Assert.That(client.GetInfo(150), Is.EqualTo(mewtwo));
            }
        }
    }   
}