using Moq;
using NUnit.Framework;
using Pokedex.API.Data;
using Pokedex.API.Clients;
using Pokedex.API.Managers;
using System.Threading.Tasks;

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
            clientMock.Invocations.Clear();
            manager = new PokedexManager(clientMock.Object);
        }

        [Test]
        public void GetPokemonFromIdAsync_CallsClientGetInfoAsync()
        {
            clientMock.Setup(cm => cm.GetInfoAsync(It.IsAny<int>()));

            manager.GetPokemonFromIdAsync(1);

            clientMock.Verify(cm => cm.GetInfoAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void GetPokemonFromIdAsync_ReturnsPokemon()
        {
            clientMock.Setup(cm => cm.GetInfoAsync(It.IsAny<int>())).Returns(Task.FromResult(Data.Constants.FakeExpectedJson));
            var res = manager.GetPokemonFromIdAsync(1).Result;

            Assert.AreEqual(Data.Constants.FakePokemon, res);
        }

        [Test]
        public void GetTranslatedPokemonFromIdAsync_CallsClientGetInfoAsyncAndGetTranslatedTextAsync()
        {
            clientMock.Setup(cm => cm.GetInfoAsync(It.IsAny<int>())).Returns(Task.FromResult(Data.Constants.FakeExpectedJson));
            clientMock.Setup(cm => cm.GetTranslatedTextAsync(It.IsAny<string>()));

            manager.GetTranslatedPokemonFromIdAsync(1);

            clientMock.Verify(cm => cm.GetInfoAsync(It.IsAny<int>()), Times.Once);
            clientMock.Verify(cm => cm.GetTranslatedTextAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetTranslatedPokemonFromIdAsync_ReturnsTranslatedPokemon()
        {
            Pokemon expectedPokemon = Data.Constants.FakePokemon;
            string fakeTranslation = "A test pokemon description, this is.";
            expectedPokemon.Description = fakeTranslation;

            clientMock.Setup(cm => cm.GetInfoAsync(It.IsAny<int>())).Returns(Task.FromResult(Data.Constants.FakeExpectedJson));
            clientMock.Setup(cm => cm.GetTranslatedTextAsync(It.IsAny<string>())).Returns(Task.FromResult(fakeTranslation));

            Pokemon res = manager.GetTranslatedPokemonFromIdAsync(1).Result;

            Assert.AreEqual(expectedPokemon, res);
        }
    }
}