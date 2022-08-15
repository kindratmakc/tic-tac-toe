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
            { '0', '0', '0' },
            { '0', '0', '0' },
            { '0', '0', '0' },
        }, scoredBoard);
    }

    [Fact]
    public void score_second_move_of_minimizing_player()
    {
        var game = new Game();

        GameTest.ApplyMoves(
            new[] {new Spot(0, 0)},
            new Spot[] {},
            game
        );
        var scoredBoard = CreateScoredBoard(game, false);

        Assert.Equal(new[,]
        {
            { 'x', '3', '3' },
            { '3', '0', '3' },
            { '3', '3', '3' },
        }, scoredBoard);
    }

    private static char[,] CreateScoredBoard(Game game, bool isMaximizing)
    {
        var scoredMoves = new Minimax().ScoreBoard(game, isMaximizing);
        var scoredBoard = (char[,])game.GetBoard().Clone();

        foreach (var scoredMove in scoredMoves)
        {
            scoredBoard[scoredMove.Spot.Y, scoredMove.Spot.X] = scoredMove.Score.ToString()[0];
        }

        return scoredBoard;
    }
}