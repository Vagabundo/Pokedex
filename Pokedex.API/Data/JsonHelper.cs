using System;
using System.Text.RegularExpressions;
using ServiceStack;

namespace Pokedex.API.Data
{
    public static class JsonHelper
    {
        public static string GetPokemonName(string text)
        {
            dynamic json = DynamicJson.Deserialize(text);
            return json?.name;
        }

        public static string GetPokemonDescription(string text)
        {
            dynamic json = DynamicJson.Deserialize(text);
            return Regex.Replace(json?.flavor_text_entries[0]?.flavor_text, "\n|\r|\f|\b|\t", " ");
        }

        public static string GetPokemonTranslatedDescription(string text)
        {
            dynamic json = DynamicJson.Deserialize(text);
            // The responses from translations API sometimes include double spaces after comma in the strings, so I have to filter it 
            return json?.success?.total == "0" ? null : Regex.Replace(json?.contents?.translated, "  ", " ");
        }

        public static string GetPokemonHabitat(string text)
        {
            dynamic json = DynamicJson.Deserialize(text);
            return json?.habitat?.name;
        }

        public static bool GetPokemonIsLegendary(string text)
        {
            dynamic json = DynamicJson.Deserialize(text);
            return json?.is_legendary == "true";
        }

        public static int GetPokemonId(string text)
        {
            int id;
            dynamic json = DynamicJson.Deserialize(text);
            
            return Int32.TryParse(json?.id, out id) ? id : 0;
        }
    }
}