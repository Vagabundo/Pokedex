using NUnit.Framework;
using Pokedex.API.Data;
using Pokedex.API.Clients;
using ServiceStack;
using Moq;
using Microsoft.Extensions.Configuration;

namespace Pokedex.API.Test.Clients
{
    [TestFixture]
    public class ServiceStackClientTest
    {
        private ServiceStackClient client;
        private const string returnedJson =
        "{\"base_happiness\":0,\"capture_rate\":3,\"color\":{\"name\":\"purple\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/pokemon-color\\/7\\/\"},"+
        "\"egg_groups\":[{\"name\":\"no-eggs\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/egg-group\\/15\\/\"}],\"evolution_chain\":{\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/evolution-chain\\/77\\/\"},"+
        "\"evolves_from_species\":null,\"flavor_text_entries\":[{\"flavor_text\":\"It was created by\\na scientist after\\nyears of horrific\\fgene splicing and\\nDNA engineering\\nexperiments.\","+
        "\"language\":{\"name\":\"en\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/language\\/9\\/\"},\"version\":{\"name\":\"red\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/version\\/1\\/\"}},"+
        "{\"flavor_text\":\"It was created by\\na scientist after\\nyears of horrific\\fgene splicing and\\nDNA engineering\\nexperiments.\","+
        "\"language\":{\"name\":\"en\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/language\\/9\\/\"},\"version\":{\"name\":\"blue\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/version\\/2\\/\"}}],"+
        "\"habitat\":{\"name\":\"rare\",\"url\":\"https:\\/\\/pokeapi.co\\/api\\/v2\\/pokemon-habitat\\/5\\/\"},\"has_gender_differences\":false,\"hatch_counter\":120,\"id\":150,\"is_baby\":false,"+
        "\"is_legendary\":true,\"is_mythical\":false,\"name\":\"mewtwo\"}";

        [SetUp]
        public void Setup()
        {

            Mock<IConfigurationSection> mockUrl = new Mock<IConfigurationSection>();
            Mock<IConfigurationSection> mockAgent = new Mock<IConfigurationSection>();
            mockUrl.Setup(x=>x.Value).Returns("http://test.test");
            mockAgent.Setup(x=>x.Value).Returns("test-agent");


            var configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetSection("PokeApiUrl")).Returns(mockUrl.Object);
            configuration.Setup(c => c.GetSection("UserAgent")).Returns(mockAgent.Object);
            
            client = new ServiceStackClient(configuration.Object);
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

            using (new HttpResultsFilter{ StringResult = returnedJson })
            {
                Assert.That(client.GetInfo(150) == mewtwo, "GetInfo does not return the expected object");
            }
        }
    }   
}