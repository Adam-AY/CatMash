namespace CatMash.Api.Models
{
    public class Vote
    {
        public required string WinnerId { get; set; }
        public required string LoserId { get; set; }
    }
}
