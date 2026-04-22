namespace CatMash.Api.Models;

public class Cat
{
    public required string Id { get; set; }
    public required string Url { get; set; }
    public int Score { get; set; } = 0;
}
