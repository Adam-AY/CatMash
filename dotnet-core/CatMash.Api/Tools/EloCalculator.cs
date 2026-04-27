namespace CatMash.Api.Tools
{
    public static class EloCalculator
    {
        public static (double winnerScore, double loserScore) Calculate(double winnerScore, double loserScore, int k = 32)
        {
            double expectedWinner = 1.0 / (1 + Math.Pow(10, (loserScore - winnerScore) / 400.0));
            double expectedLoser = 1.0 / (1 + Math.Pow(10, (winnerScore - loserScore) / 400.0));

            double newWinner = winnerScore + (k * (1 - expectedWinner));
            double newLoser = loserScore + (k * (0 - expectedLoser));

            return (newWinner, newLoser);
        }
    }
}
