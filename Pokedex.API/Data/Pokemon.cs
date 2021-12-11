using System;

namespace Pokedex.API.Data
{
    public class Pokemon
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Habitat { get; set; }
        public bool IsLegendary { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Pokemon pokemon &&
                   Name == pokemon.Name &&
                   Description == pokemon.Description &&
                   Habitat == pokemon.Habitat &&
                   IsLegendary == pokemon.IsLegendary;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Habitat, IsLegendary);
        }

        public static bool operator ==(Pokemon left, Pokemon right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Pokemon left, Pokemon right)
        {
            return !Equals(left, right);
        }
    }
}