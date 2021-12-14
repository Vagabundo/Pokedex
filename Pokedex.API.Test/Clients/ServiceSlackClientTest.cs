using NUnit.Framework;
using Pokedex.API.Clients;
using ServiceStack;
using static Pokedex.API.Test.Data.Constants;

namespace Pokedex.API.Test.Clients
{
    [TestFixture]
    public class ServiceStackClientTest
    {
        private ServiceStackClient client;

        [SetUp]
        public void Setup()
        {       
            client = new ServiceStackClient(FakeClientConfig);
        }

        [Test]
        public void GetPokemonInfoFromNameAsync_UsesServiceStackHttpClientAndReturnsExpectedJson()
        {
            using (new HttpResultsFilter{ StringResult = FakePokemonFromNameJson })
            {
                Assert.That(client.GetPokemonInfoFromNameAsync(FakePokemon.Name).Result == FakePokemonFromNameJson);
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
        public void GetTranslatedTextAsync_UsesServiceStackHttpClientAndReturnsTranslatedDescription()
        {
            using (new HttpResultsFilter{ StringResult = FakeTranslationJson })
            {
                Assert.That(client.GetTranslatedTextAsync(FakePokemon.Description, true).Result == FakeTranslationJson);
            }
        }
    }
}