using NUnit.Framework;
using Pokedex.API.Clients;
using ServiceStack;
using Moq;
using Microsoft.Extensions.Configuration;
using static Pokedex.API.Test.Data.Constants;

namespace Pokedex.API.Test.Clients
{
    [TestFixture]
    public class ServiceStackClientTest
    {
        private ServiceStackClient client;
        private Mock<IConfigurationSection> mockSection;
        private Mock<IConfiguration> configuration;

        [SetUp]
        public void Setup()
        {
            mockSection = new Mock<IConfigurationSection>();
            configuration = new Mock<IConfiguration>();
            mockSection.Setup(x=>x.Value).Returns("http://test.test/");
            configuration.Setup(c => c.GetSection(It.IsAny<string>())).Returns(mockSection.Object);
            
            client = new ServiceStackClient(configuration.Object);
        }

        [Test]
        public void GetPokemonInfoFromNameAsync_UsesServiceStackHttpClientAndReturnsExpectedJson()
        {
            using (new HttpResultsFilter{ StringResult = FakePokemonFromNameJson })
            {
                Assert.That(client.GetPokemonInfoFromNameAsync("test").Result == FakePokemonFromNameJson);
            }
        }

        [Test]
        public void GetPokemonInfoFromIdAsync_UsesServiceStackHttpClientAndReturnsExpectedJson()
        {
            using (new HttpResultsFilter{ StringResult = FakePokemonJson })
            {
                Assert.That(client.GetPokemonInfoFromIdAsync(PokemonId).Result == FakePokemonJson);
            }
        }

        [Test]
        public void GetTranslatedTextAsync_UsesServiceStackHttpClientAndReturnsExpectedJson()
        {
            using (new HttpResultsFilter{ StringResult = FakeTranslationJson })
            {
                Assert.That(client.GetTranslatedTextAsync("testing http client").Result == FakeTranslationJson);
            }
        }
    }   
}