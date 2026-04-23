namespace CatMash.Api.Models;

public class Cat
{
    public required string Id { get; set; }
    public required string Url { get; set; }
    public double Score { get; set; } = 1200;

}
