using GameRules;
using Xunit.Abstractions;

namespace GameRulesTest;

public class MinimaxTest
{
    [Fact]
    public void score_first_move_for_maximizing()
    {
        var game = new Game();

        var scoredBoard = CreateScoredBoard(game, true);

        Assert.Equal(new[,]
        {
            { "0:4", "0:2", "0:4" },
            { "0:2", "0:0", "0:2" },
            { "0:4", "0:2", "0:4" },
        }, scoredBoard);
    }

    [Fact]
    public void score_second_move_of_minimizing_player()
    {
        var game = new Game();
        game.Check(new Spot(0, 0));

        var scoredBoard = CreateScoredBoard(game, false);
        
        Assert.Equal(new[,]
        {
            { "x", "3:7", "3:6" },
            { "3:7", "0:4", "3:7" },
            { "3:6", "3:7", "3:6" },
        }, scoredBoard);
    }

    private static string[,] CreateScoredBoard(Game game, bool isMaximizing)
    {
        var scoredMoves = new Minimax().ScoreBoard(game, isMaximizing);
        var gameBoard = game.GetBoard();
        var scoredBoard = new string[3, 3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                scoredBoard[i, j] = gameBoard[i, j].ToString();
            }
        }

        foreach (var scoredMove in scoredMoves)
        {
            scoredBoard[scoredMove.Spot.Y, scoredMove.Spot.X] =
                $"{scoredMove.Score.ToString()[0]}:{scoredMove.OpportunityScore.ToString()[0]}";
        }

        return scoredBoard;
    }
}