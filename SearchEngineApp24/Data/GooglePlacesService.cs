using Newtonsoft.Json;
using SearchEngineApp24.Models;

namespace SearchEngineApp24.Data
{
    public class GooglePlacesService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _customSearchEngineId;

        public GooglePlacesService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["googleApiKey"];
            _customSearchEngineId = configuration["searchEngineId"];
        }

        public async Task<IEnumerable<PizzaStore>> SearchPlacesAsync(string query)
        {
            var response = await _httpClient.GetStringAsync($"https://www.googleapis.com/customsearch/v1?q={query}&cx={_customSearchEngineId}&key={_apiKey}");
            var searchResults = JsonConvert.DeserializeObject<GoogleCustomSearchResponse>(response);

            // Limit the results to only 10 items
            return searchResults.Items.Take(10).Select(item => new PizzaStore
            {
                Name = item.Title,
                Address = item.Snippet, // Custom Search API doesn't provide structured address, use snippet
                City = ExtractCityFromSnippet(item.Snippet)
            });
        }

        private string ExtractCityFromSnippet(string snippet)
        {
            // Implement logic to extract the city from the snippet if possible
            // This is a placeholder implementation
            var city = snippet.Split(',').FirstOrDefault();
            return city?.Trim();
        }
    }

    public class GooglePlacesResponse
    {
        public List<GooglePlaceResult> Results { get; set; }
    }

    public class GooglePlaceResult
    {
        public string Name { get; set; }
        public string FormattedAddress { get; set; }
        public List<AddressComponent> AddressComponents { get; set; }
    }

    public class AddressComponent
    {
        public string LongName { get; set; }
        public List<string> Types { get; set; }
    }

    public class GoogleCustomSearchResponse
    {
        [JsonProperty("items")]
        public List<SearchResultItem> Items { get; set; }
    }

    public class SearchResultItem
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("snippet")]
        public string Snippet { get; set; }

        // Add other properties if needed
    }
}
