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
        private Mock<IConfigurationSection> mockPokemonUrl;
        private Mock<IConfigurationSection> mockAgent;
        private Mock<IConfigurationSection> mockTranslationUrl;
        private Mock<IConfiguration> configuration;

        [SetUp]
        public void Setup()
        {
            mockAgent = new Mock<IConfigurationSection>();
            configuration = new Mock<IConfiguration>();

            mockAgent.Setup(x=>x.Value).Returns("test-agent");
            configuration.Setup(c => c.GetSection("UserAgent")).Returns(mockAgent.Object);
            
            client = new ServiceStackClient(configuration.Object);
        }

        [Test]
        public void GetInfoAsync_UsesServiceStackHttpClientAndReturnsExpectedJson()
        {
            mockPokemonUrl = new Mock<IConfigurationSection>();
            mockPokemonUrl.Setup(x=>x.Value).Returns("http://test.test");
            configuration.Setup(c => c.GetSection("PokeApiUrl")).Returns(mockPokemonUrl.Object);

            using (new HttpResultsFilter{ StringResult = FakePokemonJson })
            {
                Assert.That(client.GetInfoAsync(1).Result == FakePokemonJson);
            }
        }

        [Test]
        public void GetTranslatedTextAsync_UsesServiceStackHttpClientAndReturnsExpectedJson()
        {
            mockTranslationUrl = new Mock<IConfigurationSection>();
            mockTranslationUrl.Setup(x=>x.Value).Returns("http://test.test");
            configuration.Setup(c => c.GetSection("YodaTranslationApiUrl")).Returns(mockTranslationUrl.Object);

            using (new HttpResultsFilter{ StringResult = FakeTranslationJson })
            {
                Assert.That(client.GetTranslatedTextAsync("testing http client").Result == FakeTranslationJson);
            }
        }
    }   
}