namespace GameRules;

public class Minimax
{
    public List<ScoredMove> ScoreBoard(IBoardProvider board, bool isMaximizing)
    {
        var moves = new List<ScoredMove>();
        foreach (var spot in board.GetEmptySpots())
        {
            var clonedBoard = (IBoardProvider)board.Clone();
            clonedBoard.Check(spot);
            var score = Score(clonedBoard, !isMaximizing);
            moves.Add(new ScoredMove(spot, score));
        }

        return moves;
    }

    public int Score(IBoardProvider board, bool isMaximizing)
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

public struct ScoredMove
{
    public Spot Spot { get; }
    public int Score { get;  }

    public ScoredMove(Spot spot, int score)
    {
        Spot = spot;
        Score = score;
    }

    public override string ToString()
    {
        return $"{Spot} - {Score}";
    }
}