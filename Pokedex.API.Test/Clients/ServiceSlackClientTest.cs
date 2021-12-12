using NUnit.Framework;
using Pokedex.API.Data;
using Pokedex.API.Clients;
using ServiceStack;
using Pokedex.API.Test.Data;
using Moq;
using Microsoft.Extensions.Configuration;

namespace Pokedex.API.Test.Clients
{
    [TestFixture]
    public class ServiceStackClientTest
    {
        private ServiceStackClient client;

        [SetUp]
        public void Setup()
        {
            Mock<IConfigurationSection> mockUrl = new Mock<IConfigurationSection>();
            Mock<IConfigurationSection> mockAgent = new Mock<IConfigurationSection>();
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();

            mockUrl.Setup(x=>x.Value).Returns("http://test.test");
            mockAgent.Setup(x=>x.Value).Returns("test-agent");

            configuration.Setup(c => c.GetSection("PokeApiUrl")).Returns(mockUrl.Object);
            configuration.Setup(c => c.GetSection("UserAgent")).Returns(mockAgent.Object);
            
            client = new ServiceStackClient(configuration.Object);
        }

        [Test]
        public void GetInfo_ReturnsExpectedPokemonObject()
        {
            using (new HttpResultsFilter{ StringResult = Constants.FakeExpectedJson })
            {
                Assert.That(client.GetInfoAsync(150).Result == Constants.FakeExpectedJson);
            }
        }
    }   
}