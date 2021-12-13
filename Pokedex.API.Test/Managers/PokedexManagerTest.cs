using Moq;
using NUnit.Framework;
using Pokedex.API.Data;
using Pokedex.API.Clients;
using Pokedex.API.Managers;
using System.Threading.Tasks;
using static Pokedex.API.Test.Data.Constants;

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
        public async Task GetPokemonFromNameAsync_CallsClientGetPokemonInfoFromNameAsync()
        {
            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).Returns(Task.FromResult(FakePokemonFromNameJson));
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakePokemonJson));
            
            await manager.GetPokemonFromNameAsync(FakePokemon.Name);

            clientMock.Verify(cm => cm.GetPokemonInfoFromNameAsync(FakePokemon.Name), Times.Once);
            clientMock.Verify(cm => cm.GetPokemonInfoFromIdAsync(PokemonId), Times.Once);
        }

        [Test]
        public void GetPokemonFromNameAsync_ReturnsPokemon()
        {
            Pokemon result;
            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).Returns(Task.FromResult(FakePokemonFromNameJson));
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakePokemonJson));
            
            result = manager.GetPokemonFromNameAsync(FakePokemon.Name).Result.Body;

            Assert.AreEqual(FakePokemon, result);
        }

        [Test]
        public async Task GetPokemonFromIdAsync_CallsClientGetPokemonInfoFromIdAsync()
        {
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakePokemonJson));

            await manager.GetPokemonFromIdAsync(PokemonId);

            clientMock.Verify(cm => cm.GetPokemonInfoFromIdAsync(PokemonId), Times.Once);
        }

        [Test]
        public void GetPokemonFromIdAsync_ReturnsPokemon()
        {
            Pokemon result;
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakePokemonJson));

            result = manager.GetPokemonFromIdAsync(PokemonId).Result.Body;

            Assert.AreEqual(FakePokemon, result);
        }

        [Test]
        public async Task GetTranslatedPokemonFromNameAsync_CallsClientGetPokemonInfoFromNameAsyncAndGetTranslatedTextAsync()
        {
            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).Returns(Task.FromResult(FakePokemonFromNameJson));
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakePokemonJson));
            clientMock.Setup(cm => cm.GetTranslatedTextAsync(It.IsAny<string>())).Returns(Task.FromResult(FakeTranslationJson));

            await manager.GetTranslatedPokemonFromNameAsync(FakePokemon.Name);

            clientMock.Verify(cm => cm.GetPokemonInfoFromNameAsync(FakePokemon.Name), Times.Once);
            clientMock.Verify(cm => cm.GetTranslatedTextAsync(FakePokemon.Description), Times.Once);
        }

        [Test]
        public void GetTranslatedPokemonFromNameAsync_ReturnsTranslatedPokemon()
        {
            Pokemon result;
            Pokemon expectedPokemon = FakePokemon;
            expectedPokemon.Description = FakeTranslation;

            clientMock.Setup(cm => cm.GetPokemonInfoFromNameAsync(It.IsAny<string>())).Returns(Task.FromResult(FakePokemonFromNameJson));
            clientMock.Setup(cm => cm.GetPokemonInfoFromIdAsync(It.IsAny<int>())).Returns(Task.FromResult(FakePokemonJson));
            clientMock.Setup(cm => cm.GetTranslatedTextAsync(It.IsAny<string>())).Returns(Task.FromResult(FakeTranslationJson));

            result = manager.GetTranslatedPokemonFromNameAsync(FakePokemon.Name).Result.Body;

            Assert.AreEqual(expectedPokemon, result);
        }
    }
}