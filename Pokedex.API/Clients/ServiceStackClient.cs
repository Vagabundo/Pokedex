using Pokedex.API.Data;

namespace Pokedex.API.Clients
{
    public class ServiceStackClient : IPokemonClient
    {
        public Pokemon GetInfo(int id)
        {
            return new Pokemon {
                Name = "Simba",
                Description = "The lion king",
                Habitat = "African sabanah",
                IsLegendary = false
            };
        }
    }
}