
using CatMash.Api.Models;
using System.Text.Json;

namespace CatMash.Api;

public class CatService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public List<Cat> Cats { get; private set; } = new();

    public CatService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        LoadCats().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Load Cats from Atelier Url
    /// </summary>
    /// <returns></returns>
    private async Task LoadCats()
    {
        var http = _httpClientFactory.CreateClient();
        var json = await http.GetStringAsync("https://conseil.latelier.co/data/cats.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = JsonSerializer.Deserialize<CatResponse>(json, options);

        Cats = result?.Images ?? new List<Cat>();
    }
}
