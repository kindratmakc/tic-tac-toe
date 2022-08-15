using GameRules;

namespace GameRulesTest;

public class GameTest
{
    [Fact]
    public void game_can_be_created()
    {
        var game = CreateGame();

        Assert.Equal(new[,]
        {
            {'_', '_', '_'},
            {'_', '_', '_'},
            {'_', '_', '_'},
        }, game.GetBoard());
        Assert.Equal(State.Ongoing, game.GetState());
    }

    [Fact]
    public void get_empty_spots()
    {
        var game = CreateGame();

        // {'x', 'o', '_'},
        // {'x', 'o', '_'},
        // {'x', '_', '_'},


        ApplyMoves(
            new[] {new Spot(0, 0), new Spot(0, 1), new Spot(0, 2)},
            new[] {new Spot(1, 0), new Spot(1, 1)},
            game
        );

        var expected = new Spot[] {new(2, 0), new(2, 1), new(1, 2), new(2, 2)};
        var actual = game.GetEmptySpots();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void first_player_checks_with_x()
    {
        var game = CreateGame();

        game.Check(new Spot(0, 0));

        Assert.Equal(new[,]
        {
            {'x', '_', '_'},
            {'_', '_', '_'},
            {'_', '_', '_'},
        }, game.GetBoard());
    }

    [Fact]
    public void second_player_checks_with_nought()
    {
        var game = CreateGame();

        game.Check(new Spot(0, 0));
        game.Check(new Spot(1, 0));

        Assert.Equal(new[,]
        {
            {'x', 'o', '_'},
            {'_', '_', '_'},
            {'_', '_', '_'},
        }, game.GetBoard());
    }

    [Fact]
    public void can_not_check_the_same_spot()
    {
        var game = CreateGame();
        game.Check(new Spot(0, 0));

        Assert.Throws<CanNotCheckTheSameSpotException>(() => game.Check(new Spot(0, 0)));
    }

    [Fact]
    public void can_not_check_after_a_win()
    {
        var game = CreateGame();

        ApplyMoves(
            new[] {new Spot(0, 0), new Spot(0, 1), new Spot(0, 2)},
            new[] {new Spot(1, 0), new Spot(1, 1)},
            game
        );

        Assert.Throws<CanNotCheckAfterAWinException>(() => game.Check(new Spot(1, 1)));
    }

    [Fact]
    public void can_not_check_after_a_draw()
    {
        var game = CreateGame();

        ApplyMoves(
            new[] {new Spot(0, 0), new Spot(0, 2), new Spot(1, 0), new Spot(2, 1), new Spot(2, 2)},
            new[] {new Spot(0, 1), new Spot(1, 1), new Spot(1, 2), new Spot(2, 0)},
            game
        );

        Assert.Equal(new[,]
        {
            {'x', 'x', 'o'},
            {'o', 'o', 'x'},
            {'x', 'o', 'x'},
        }, game.GetBoard());
        Assert.Throws<CanNotCheckAfterAWinException>(() => game.Check(new Spot(1, 1)));
    }

    [Fact]
    public void draw_if_no_possible_turns_left()
    {
        var game = CreateGame();

        ApplyMoves(
            new[] {new Spot(0, 0), new Spot(0, 2), new Spot(1, 0), new Spot(2, 1), new Spot(2, 2)},
            new[] {new Spot(0, 1), new Spot(1, 1), new Spot(1, 2), new Spot(2, 0)},
            game
        );

        Assert.Equal(new[,]
        {
            {'x', 'x', 'o'},
            {'o', 'o', 'x'},
            {'x', 'o', 'x'},
        }, game.GetBoard());
        Assert.Equal(State.Draw, game.GetState());
    }

    [Theory]
    [MemberData(nameof(WinCombinations))]
    public void win_combinations(string name, Spot[] p1Moves, Spot[] p2Moves, State expectedWinner, char[,] expectedState)
    {
        var game = CreateGame();

        ApplyMoves(p1Moves, p2Moves, game);

        Assert.Equal(expectedState, game.GetBoard());
        Assert.Equal(expectedWinner, game.GetState());
    }

    public static IEnumerable<object[]> WinCombinations =>
        new List<object[]>
        {
            // P1(x) Columns
            new object[]
            {
                "P1(x) first column",
                new[] {new Spot(0, 0), new Spot(0, 1), new Spot(0, 2)},
                new[] {new Spot(1, 0), new Spot(1, 1)},
                State.WinX,
                new[,]
                {
                    {'x', 'o', '_'},
                    {'x', 'o', '_'},
                    {'x', '_', '_'},
                }
            },
            new object[]
            {
                "P1(x) second column",
                new[] {new Spot(1, 0), new Spot(1, 1), new Spot(1, 2)},
                new[] {new Spot(0, 0), new Spot(0, 1)},
                State.WinX,
                new[,]
                {
                    {'o', 'x', '_'},
                    {'o', 'x', '_'},
                    {'_', 'x', '_'},
                }
            },
            new object[]
            {
                "P1(x) third column",
                new[] {new Spot(2, 0), new Spot(2, 1), new Spot(2, 2)},
                new[] {new Spot(0, 0), new Spot(0, 1)},
                State.WinX,
                new[,]
                {
                    {'o', '_', 'x'},
                    {'o', '_', 'x'},
                    {'_', '_', 'x'},
                }
            },
            // P2(o) Columns
            new object[]
            {
                "P2(o) first column",
                new[] {new Spot(1, 0), new Spot(1, 1), new Spot(2, 2)},
                new[] {new Spot(0, 0), new Spot(0, 1), new Spot(0, 2)},
                State.WinO,
                new[,]
                {
                    {'o', 'x', '_'},
                    {'o', 'x', '_'},
                    {'o', '_', 'x'},
                }
            },
            new object[]
            {
                "P2(o) second column",
                new[] {new Spot(0, 0), new Spot(0, 1), new Spot(2, 2)},
                new[] {new Spot(1, 0), new Spot(1, 1), new Spot(1, 2)},
                State.WinO,
                new[,]
                {
                    {'x', 'o', '_'},
                    {'x', 'o', '_'},
                    {'_', 'o', 'x'},
                }
            },
            new object[]
            {
                "P2(o) third column",
                new[] {new Spot(0, 0), new Spot(0, 1), new Spot(2, 2)},
                new[] {new Spot(1, 0), new Spot(1, 1), new Spot(1, 2)},
                State.WinO,
                new[,]
                {
                    {'x', 'o', '_'},
                    {'x', 'o', '_'},
                    {'_', 'o', 'x'},
                }
            },
            // P1(x) Rows
            new object[]
            {
                "P1(x) first row",
                new[] {new Spot(0, 0), new Spot(1, 0), new Spot(2, 0)},
                new[] {new Spot(0, 1), new Spot(1, 1)},
                State.WinX,
                new[,]
                {
                    {'x', 'x', 'x'},
                    {'o', 'o', '_'},
                    {'_', '_', '_'},
                }
            },
            new object[]
            {
                "P1(x) second row",
                new[] {new Spot(0, 1), new Spot(1, 1), new Spot(2, 1)},
                new[] {new Spot(0, 0), new Spot(1, 0)},
                State.WinX,
                new[,]
                {
                    {'o', 'o', '_'},
                    {'x', 'x', 'x'},
                    {'_', '_', '_'},
                }
            },
            new object[]
            {
                "P1(x) third row",
                new[] {new Spot(0, 2), new Spot(1, 2), new Spot(2, 2)},
                new[] {new Spot(0, 0), new Spot(1, 1)},
                State.WinX,
                new[,]
                {
                    {'o', '_', '_'},
                    {'_', 'o', '_'},
                    {'x', 'x', 'x'},
                }
            },
            // P2(o) Rows
            new object[]
            {
                "P2(o) first row",
                new[] {new Spot(0, 2), new Spot(1, 1), new Spot(2, 2)},
                new[] {new Spot(0, 0), new Spot(1, 0), new Spot(2, 0)},
                State.WinO,
                new[,]
                {
                    {'o', 'o', 'o'},
                    {'_', 'x', '_'},
                    {'x', '_', 'x'},
                }
            },
            new object[]
            {
                "P2(o) second row",
                new[] {new Spot(0, 2), new Spot(1, 0), new Spot(2, 2)},
                new[] {new Spot(0, 1), new Spot(1, 1), new Spot(2, 1)},
                State.WinO,
                new[,]
                {
                    {'_', 'x', '_'},
                    {'o', 'o', 'o'},
                    {'x', '_', 'x'},
                }
            },
            new object[]
            {
                "P2(o) third row",
                new[] {new Spot(0, 1), new Spot(1, 0), new Spot(2, 1)},
                new[] {new Spot(0, 2), new Spot(1, 2), new Spot(2, 2)},
                State.WinO,
                new[,]
                {
                    {'_', 'x', '_'},
                    {'x', '_', 'x'},
                    {'o', 'o', 'o'},
                }
            },
            // P1(x) Diagonals
            new object[]
            {
                "P1(x) first diagonal",
                new[] {new Spot(0, 0), new Spot(1, 1), new Spot(2, 2)},
                new[] {new Spot(0, 1), new Spot(2, 0)},
                State.WinX,
                new[,]
                {
                    {'x', '_', 'o'},
                    {'o', 'x', '_'},
                    {'_', '_', 'x'},
                }
            },
            new object[]
            {
                "P1(x) second diagonal",
                new[] {new Spot(0, 2), new Spot(1, 1), new Spot(2, 0)},
                new[] {new Spot(0, 0), new Spot(2, 2)},
                State.WinX,
                new[,]
                {
                    {'o', '_', 'x'},
                    {'_', 'x', '_'},
                    {'x', '_', 'o'},
                }
            },
            // P2(o) Diagonals
            new object[]
            {
                "P2(o) first diagonal",
                new[] {new Spot(0, 1), new Spot(1, 2), new Spot(2, 0)},
                new[] {new Spot(0, 0), new Spot(1, 1), new Spot(2, 2)},
                State.WinO,
                new[,]
                {
                    {'o', '_', 'x'},
                    {'x', 'o', '_'},
                    {'_', 'x', 'o'},
                }
            },
            new object[]
            {
                "P2(o) second diagonal",
                new[] {new Spot(0, 0), new Spot(2, 2), new Spot(1, 0)},
                new[] {new Spot(0, 2), new Spot(1, 1), new Spot(2, 0)},
                State.WinO,
                new[,]
                {
                    {'x', 'x', 'o'},
                    {'_', 'o', '_'},
                    {'o', '_', 'x'},
                }
            },
        };

    private static Spot[] MergeMoves(Spot[] p1Moves, Spot[] p2Moves)
    {
        var p1MovesQueue = new Queue<Spot>(p1Moves);
        var p2MovesQueue = new Queue<Spot>(p2Moves);

        var totalMoves = p1Moves.Length + p2Moves.Length;
        var moves = new Spot[totalMoves];

        for (int i = 0; i < totalMoves; i++)
        {
            var nextMove = i % 2 == 0
                ? p1MovesQueue.Dequeue()
                : p2MovesQueue.Dequeue();

            moves[i] = nextMove;
        }

        return moves;
    }

    public static void ApplyMoves(Spot[] p1Moves, Spot[] p2Moves, Game game)
    {
        var moves = MergeMoves(p1Moves, p2Moves);

        foreach (var move in moves)
        {
            game.Check(move);
        }
    }

    private static Game CreateGame()
    {
        return new Game();
    }
}