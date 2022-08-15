namespace GameRules;

public class Minimax
{
    private static Dictionary<string, IEnumerable<ScoredMove>> _cache = new();

    public IEnumerable<ScoredMove> ScoreBoard(IBoardProvider board, bool isMaximizing)
    {
        if (_cache.ContainsKey(board.ToString()))
        {
            return _cache[board.ToString()];
        }
        if (board.GetState() != State.Ongoing)
        {
            return new List<ScoredMove>();
        }
        
        var moves = new List<ScoredMove>();
        foreach (var spot in board.GetEmptySpots())
        {
            var clonedBoard = (IBoardProvider)board.Clone();
            clonedBoard.Check(spot);
            var score = Score(clonedBoard, !isMaximizing);
            var sb = ScoreBoard(clonedBoard, isMaximizing);
            
            moves.Add(new ScoredMove(spot, score, sb.Sum(x => x.Score)));
        }

        _cache[board.ToString()] = moves;

        return moves;
    }  

    private int Score(IBoardProvider board, bool isMaximizing)
    {
        if (board.GetState() != State.Ongoing)
        {
            return Eval(board);
        }

        if (isMaximizing)
        {
            var maxEval = -100;
            foreach (var spot in board.GetEmptySpots())
            {
                var clonedBoard = (IBoardProvider)board.Clone();
                clonedBoard.Check(spot);
                var eval = Score(clonedBoard, false);
                maxEval = Math.Max(maxEval, eval);
            }
            return maxEval;
        }

        var minEval = 100;
        foreach (var spot in board.GetEmptySpots())
        {
            var clonedBoard = (IBoardProvider)board.Clone();
            clonedBoard.Check(spot);
            var eval = Score(clonedBoard, true);
            minEval = Math.Min(minEval, eval);
        }

        return minEval;
    }

    private int Eval(IBoardProvider board)
    {
        
        // board.GetBoard()
        var score = board.GetState() switch
        {
            State.Draw => 0,
            State.WinO => -1 * (board.GetEmptySpotsCount() + 1),
            State.WinX => board.GetEmptySpotsCount() + 1,
            _ => throw new InvalidOperationException(),
        };

        return score;
    }
}

public readonly struct ScoredMove
{
    public Spot Spot { get; }
    public int Score { get;  }

    public readonly int OpportunityScore;

    public ScoredMove(Spot spot, int score, int opportunityScore)
    {
        Spot = spot;
        Score = score;
        OpportunityScore = opportunityScore;
    }

    public override string ToString()
    {
        return $"{Spot} - {Score}";
    }
}