using Moq;
using NUnit.Framework;
using Pokedex.API.Data;
using Pokedex.API.Clients;
using Pokedex.API.Managers;

namespace Pokedex.API.Test.Maganers
{
    [TestFixture]
    public class PokedexManagerTest
    {
        private PokedexManager manager;
        private Mock<IPokemonClient> clientMock = new Mock<IPokemonClient>();

        [SetUp]
        public void Setup()
        {
            manager = new PokedexManager(clientMock.Object);
        }

        [Test]
        public void GetPokemonFromId_CallsClientGetInfo()
        {
            clientMock.Setup(cm => cm.GetInfo(It.IsAny<int>()));

            manager.GetPokemonFromId(2);

            clientMock.Verify(cm => cm.GetInfo(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetPokemonFromId_ReturnsPokemon()
        {
            Pokemon fakePokemon = new Pokemon {
                Name = "Test",
                Description = "tesst",
                Habitat = "test",
                IsLegendary = false
            };

            clientMock.Setup(cm => cm.GetInfo(It.IsAny<int>())).Returns(fakePokemon);

            Assert.AreEqual(fakePokemon, manager.GetPokemonFromId(2));
        }
    }
}