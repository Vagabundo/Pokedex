using Moq;
using NUnit.Framework;
using Pokedex.API.Data;
using Pokedex.API.Clients;
using Pokedex.API.Managers;
using System.Threading.Tasks;
using static Pokedex.API.Test.Data.Constants;
using Microsoft.Extensions.Logging;
using System;
using Pokedex.API.Test.Data;

namespace Pokedex.API.Test.Maganers
{
    [TestFixture]
    public class PokemonManagerTest
    {
        private PokemonManager manager;
        private Mock<IPokemonClient> clientMock;
        private Mock<ILogger<PokemonManager>> logger = new Mock<ILogger<PokemonManager>>();

        [SetUp]
        public void Setup()
        {
            clientMock = new Mock<IPokemonClient>();
            clientMock.Invocations.Clear();
            manager = new PokemonManager(clientMock.Object, logger.Object);
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
        public async Task GetTranslatedPokemonFromNameAsync_WhenLegendaryPokemon_GetsYodaTranslation()
        {
            Pokemon legendPokemon = Utils.PokemonBuilder(isLegendary: true);
            string legendJson = Utils.PokemonJsonBuilder(legendPokemon, PokemonId);
            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).ReturnsAsync(FakePokemonFromNameJson);
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).ReturnsAsync(legendJson);
            clientMock.Setup(cm => cm.GetTranslatedTextAsync(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(FakeTranslationJson);

            await manager.GetTranslatedPokemonFromNameAsync(FakePokemon.Name);

            clientMock.Verify(cm => cm.GetPokemonInfoFromNameAsync(FakePokemon.Name), Times.Once);
            clientMock.Verify(cm => cm.GetTranslatedTextAsync(FakePokemon.Description, true), Times.Once);
        }

        [Test]
        public async Task GetTranslatedPokemonFromNameAsync_WhenCavePokemon_GetsYodaTranslation()
        {
            Pokemon cavePokemon = Utils.PokemonBuilder(habitat: "cave");
            string caveJson = Utils.PokemonJsonBuilder(cavePokemon, PokemonId);
            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).ReturnsAsync(FakePokemonFromNameJson);
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).ReturnsAsync(caveJson);
            clientMock.Setup(cm => cm.GetTranslatedTextAsync(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(FakeTranslationJson);

            await manager.GetTranslatedPokemonFromNameAsync(cavePokemon.Name);

            clientMock.Verify(cm => cm.GetPokemonInfoFromNameAsync(cavePokemon.Name), Times.Once);
            clientMock.Verify(cm => cm.GetTranslatedTextAsync(cavePokemon.Description, true), Times.Once);
        }

        [Test]
        public async Task GetTranslatedPokemonFromNameAsync_WhenNoCaveNorLegendaryPokemon_GetsShakespeareTranslation()
        {
            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).ReturnsAsync(FakePokemonFromNameJson);
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).ReturnsAsync(FakePokemonJson);
            clientMock.Setup(cm => cm.GetTranslatedTextAsync(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(FakeTranslationJson);

            await manager.GetTranslatedPokemonFromNameAsync(FakePokemon.Name);

            clientMock.Verify(cm => cm.GetPokemonInfoFromNameAsync(FakePokemon.Name), Times.Once);
            clientMock.Verify(cm => cm.GetTranslatedTextAsync(FakePokemon.Description, false), Times.Once);
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
            Pokemon expectedPokemon = Utils.PokemonBuilder();
            expectedPokemon.Description = FakeTranslation;

            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).ReturnsAsync(FakePokemonFromNameJson);
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).ReturnsAsync(Utils.PokemonJsonBuilder(expectedPokemon, 1));
            clientMock.Setup(cm => cm.GetTranslatedTextAsync(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(FakeTranslationJson);

            result = (await manager.GetTranslatedPokemonFromNameAsync(expectedPokemon.Name)).Body;

            Assert.AreEqual(expectedPokemon, result);
        }
    }
}