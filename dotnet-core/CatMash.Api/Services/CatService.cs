
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

    public List<Cat> GetWinners()
    {
        try
        {
            var winners = Cats.Where(c => c.Score > 0).OrderByDescending(c => c.Score).Take(3).ToList();
            return winners;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public int GetTotalVotes()
    {
        try
        {
            var totalVotes = Cats.Sum(c => c.Score);
            return totalVotes;
        }
        catch (Exception)
        {
            throw;
        }
    }


    /// <summary>
    /// Load Cats from Atelier Url
    /// </summary>
    /// <returns></returns>
    private async Task LoadCats()
    {
        try
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
        catch (Exception)
        {
            throw;
        }
    }
}
