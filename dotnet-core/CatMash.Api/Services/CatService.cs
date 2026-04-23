
using CatMash.Api.Models;
using System.Text.Json;

namespace CatMash.Api;

public class CatService
{
    private readonly IHttpClientFactory _httpClientFactory;

    private int _totalVotes = 0;
    public int GetTotalVotes() => _totalVotes;

    public List<Cat> Cats { get; private set; } = new();

    public CatService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        LoadCats().GetAwaiter().GetResult();
    }


    public void Vote(Vote vote)
    {
        try
        {
            var winner = Cats.FirstOrDefault(c => c.Id == vote.WinnerId);
            var loser = Cats.FirstOrDefault(c => c.Id == vote.LoserId);

            if (winner == null || loser == null)
            {
                throw new Exception("Cat not found");
            }

            #region ELO 
            
            const int K = 32;

            double expectedWinner = 1 / (1 + Math.Pow(10, (loser.Score - winner.Score) / 400.0));
            double expectedLoser = 1 / (1 + Math.Pow(10, (winner.Score - loser.Score) / 400.0));

            winner.Score += K * (1 - expectedWinner);
            loser.Score += K * (0 - expectedLoser);

            #endregion

            IncrementVotes();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void IncrementVotes()
    {
        _totalVotes++;
    }

    // TO DO - à améliorer
    public List<Cat> GetWinners()
    {
        try
        {
            var winners =  Cats.Where(c => c.Score != 1200)
                               .OrderByDescending(c => c.Score)
                               .Take(3)
                               .ToList();

            return winners;
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
