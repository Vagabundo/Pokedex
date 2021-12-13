using Moq;
using NUnit.Framework;
using Pokedex.API.Data;
using Pokedex.API.Clients;
using Pokedex.API.Managers;
using System.Threading.Tasks;
using static Pokedex.API.Test.Data.Constants;
using Microsoft.Extensions.Logging;
using System;

namespace Pokedex.API.Test.Maganers
{
    [TestFixture]
    public class PokedexManagerTest
    {
        private PokedexManager manager;
        private Mock<IPokemonClient> clientMock = new Mock<IPokemonClient>();
        private Mock<ILogger<PokedexManager>> logger = new Mock<ILogger<PokedexManager>>();

        [SetUp]
        public void Setup()
        {
            clientMock.Invocations.Clear();
            manager = new PokedexManager(clientMock.Object, logger.Object);
        }

        [Test]
        public async Task GetPokemonFromNameAsync_CallsClientGetPokemonInfoFromNameAsync()
        {
            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).ReturnsAsync(FakePokemonFromNameJson);
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).ReturnsAsync(FakePokemonJson);
            
            await manager.GetPokemonFromNameAsync(FakePokemon.Name);

            clientMock.Verify(cm => cm.GetPokemonInfoFromNameAsync(FakePokemon.Name), Times.Once);
            clientMock.Verify(cm => cm.GetPokemonInfoFromIdAsync(PokemonId), Times.Once);
        }

        [Test]
        public async Task GetPokemonFromNameAsync_WhenExceptionWithNoStatusCode_ReturnsInternalError()
        {
            Response<Pokemon> result;
            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).Throws(new Exception());

            result = await manager.GetPokemonFromNameAsync(FakePokemon.Name);

            Assert.AreEqual(result.ErrorMessage, InternalErrorMessage);
        }

        [Test]
        public async Task GetPokemonFromNameAsync_ReturnsPokemon()
        {
            Pokemon result;
            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).ReturnsAsync(FakePokemonFromNameJson);
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).ReturnsAsync(FakePokemonJson);
            
            result = (await manager.GetPokemonFromNameAsync(FakePokemon.Name)).Body;

            Assert.AreEqual(FakePokemon, result);
        }

        [Test]
        public async Task GetPokemonFromIdAsync_CallsClientGetPokemonInfoFromIdAsync()
        {
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).ReturnsAsync(FakePokemonJson);

            await manager.GetPokemonFromIdAsync(PokemonId);

            clientMock.Verify(cm => cm.GetPokemonInfoFromIdAsync(PokemonId), Times.Once);
        }

        [Test]
        public async Task GetPokemonFromIdAsync_WhenExceptionWithNoStatusCode_ReturnsInternalError()
        {
            Response<Pokemon> result;
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).Throws(new Exception());

            result = await manager.GetPokemonFromIdAsync(PokemonId);

            Assert.AreEqual(result.ErrorMessage, InternalErrorMessage);
        }

        [Test]
        public async Task GetPokemonFromIdAsync_ReturnsPokemon()
        {
            Pokemon result;
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).ReturnsAsync(FakePokemonJson);

            result = (await manager.GetPokemonFromIdAsync(PokemonId)).Body;

            Assert.AreEqual(FakePokemon, result);
        }

        [Test]
        public async Task GetTranslatedPokemonFromNameAsync_CallsClientGetPokemonInfoFromNameAsyncAndGetTranslatedTextAsync()
        {
            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).ReturnsAsync(FakePokemonFromNameJson);
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).ReturnsAsync(FakePokemonJson);
            clientMock.Setup(cm => cm.GetTranslatedTextAsync(It.IsAny<string>())).ReturnsAsync(FakeTranslationJson);

            await manager.GetTranslatedPokemonFromNameAsync(FakePokemon.Name);

            clientMock.Verify(cm => cm.GetPokemonInfoFromNameAsync(FakePokemon.Name), Times.Once);
            clientMock.Verify(cm => cm.GetTranslatedTextAsync(FakePokemon.Description), Times.Once);
        }

        [Test]
        public async Task GetTranslatedPokemonFromNameAsync_WhenExceptionWithNoStatusCode_ReturnsInternalError()
        {
            Response<Pokemon> result;
            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).Throws(new Exception());

            result = await manager.GetTranslatedPokemonFromNameAsync(FakePokemon.Name);

            Assert.AreEqual(result.ErrorMessage, InternalErrorMessage);
        }

        [Test]
        public async Task GetTranslatedPokemonFromNameAsync_ReturnsTranslatedPokemon()
        {
            Pokemon result;
            Pokemon expectedPokemon = FakePokemon;
            expectedPokemon.Description = FakeTranslation;

            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).ReturnsAsync(FakePokemonFromNameJson);
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).ReturnsAsync(FakePokemonJson);
            clientMock.Setup(cm => cm.GetTranslatedTextAsync(It.IsAny<string>())).ReturnsAsync(FakeTranslationJson);

            result = (await manager.GetTranslatedPokemonFromNameAsync(FakePokemon.Name)).Body;

            Assert.AreEqual(expectedPokemon, result);
        }
    }
}