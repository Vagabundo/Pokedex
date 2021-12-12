using Moq;
using NUnit.Framework;
using Pokedex.API.Data;
using Pokedex.API.Clients;
using Pokedex.API.Managers;
using System.Threading.Tasks;
using static Pokedex.API.Test.Data.Constants;
using ServiceStack;
using System;

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
        public void GetPokemonFromNameAsync_CallsClientGetPokemonInfoFromNameAsync()
        {
            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).Returns(Task.FromResult(FakePokemonFromNameJson));
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakePokemonJson));
            dynamic json =  DynamicJson.Deserialize(FakePokemonJson);
            int id = 0;
            Int32.TryParse(json?.id, out id);
            
            manager.GetPokemonFromNameAsync("test");

            clientMock.Verify(cm => cm.GetPokemonInfoFromNameAsync("test"), Times.Once);
            clientMock.Verify(cm => cm.GetPokemonInfoFromIdAsync(id), Times.Once);
        }

        [Test]
        public void GetPokemonFromNameAsync_ReturnsPokemon()
        {
            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).Returns(Task.FromResult(FakePokemonFromNameJson));
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakePokemonJson));
            
            var res = manager.GetPokemonFromNameAsync("test").Result;

            Assert.AreEqual(FakePokemon, res);
        }

        [Test]
        public void GetPokemonFromIdAsync_CallsClientGetPokemonInfoFromIdAsync()
        {
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>()));

            manager.GetPokemonFromIdAsync(1);

            clientMock.Verify(cm => cm.GetPokemonInfoFromIdAsync(1), Times.Once);
        }

        [Test]
        public void GetPokemonFromIdAsync_ReturnsPokemon()
        {
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakePokemonJson));
            var res = manager.GetPokemonFromIdAsync(1).Result;

            Assert.AreEqual(FakePokemon, res);
        }

        [Test]
        public void GetTranslatedPokemonFromIdAsync_CallsClientGetPokemonInfoFromIdAsyncAndGetTranslatedTextAsync()
        {
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakePokemonJson));
            clientMock.Setup(cm => cm.GetTranslatedTextAsync(It.IsAny<string>()));

            manager.GetTranslatedPokemonFromIdAsync(1);

            clientMock.Verify(cm => cm.GetPokemonInfoFromIdAsync(1), Times.Once);
            clientMock.Verify(cm => cm.GetTranslatedTextAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetTranslatedPokemonFromIdAsync_ReturnsTranslatedPokemon()
        {
            Pokemon expectedPokemon = FakePokemon;
            expectedPokemon.Description = FakeTranslation;

            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakePokemonJson));
            clientMock.Setup(cm => cm.GetTranslatedTextAsync(It.IsAny<string>())).Returns(Task.FromResult(FakeTranslationJson));

            Pokemon res = manager.GetTranslatedPokemonFromIdAsync(1).Result;

            Assert.AreEqual(expectedPokemon, res);
        }
    }
}