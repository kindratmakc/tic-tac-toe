namespace GameRules;

public class MinimaxPlayer : IPlayer
{
    private readonly Random _random = new();
    private readonly Minimax _minimax = new();

    public Spot? ChooseSpot(IBoardProvider board)
    {
         var scoredMoves = _minimax.ScoreBoard(board, true);
         var bestScore = scoredMoves.Max(x => x.Score);
         var bestMoves = scoredMoves
             .Where(x => x.Score == bestScore)
             .ToList();
         var bestOpportunityScore = bestMoves.Max(x => x.OpportunityScore);
         var bestMovesWithBestOpporunities = bestMoves
             .Where(x => x.OpportunityScore == bestOpportunityScore)
             .ToList();

         return bestMovesWithBestOpporunities[_random.Next(bestMovesWithBestOpporunities.Count)].Spot;
    }
}
