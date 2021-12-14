namespace Pokedex.API.Data
{
    public class ClientDataConfig
    {
        public string PokeApiBaseUrl { get; set; }
        public string PokeApiIdUrl { get; set; }
        public string PokeApiNameUrl { get; set; }
        public TranslationsApiUrl TranslationsApiUrl { get; set; }
        public string UserAgent { get; set; }
    }

    public class TranslationsApiUrl
    {
        public string Base { get; set; }
        public string Yoda { get; set; }
        public string Shakespeare { get; set; }
        public string End { get; set; }
    }
}