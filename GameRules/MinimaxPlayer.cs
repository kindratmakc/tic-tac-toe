namespace GameRules;

public class MinimaxPlayer : IPlayer
{
    private readonly Random _random = new();
    private readonly Minimax _minimax = new Minimax();

    public Spot? ChooseSpot(IBoardProvider board)
    {
        var emptySpots = board.GetEmptySpots();

        // var scoredMoves = new List<ScoredMove>();
        Spot bestMove = default;
        var bestScore = 100;
        foreach (var spot in emptySpots)
        {
            var clonedBoard = (IBoardProvider)board.Clone();
            clonedBoard.Check(spot);
            var score = _minimax.Score(clonedBoard, true);
            // scoredMoves.Add(new ScoredMove(spot, score));
            if (score >= bestScore) continue;
            bestScore = score;
            bestMove = spot;
        }

        return bestMove;
        
        // var bestScore = scoredMoves.Min(x => x.Score);
        // var bestMoves = scoredMoves.Where(x => x.Score <= bestScore).ToList();
        
        // bestMoves.ForEach(x => Console.WriteLine(x));
        // Console.WriteLine();

        // return bestMoves[_random.Next(bestMoves.Count)].Spot;
    }
}
