namespace GameRules;

public class Game : IBoardProvider
{
    private char[,] _board;
    private State _state = State.Ongoing;

    public Game()
    {
        _board = new[,]
        {
            { '_', '_', '_' },
            { '_', '_', '_' },
            { '_', '_', '_' },
        };
    }

    public object Clone()
    {
        var game = (Game)MemberwiseClone();
        game._board = (char[,])_board.Clone();

        return game;
    }

    public char[,] GetBoard()
    {
        return _board;
    }

    public State GetState()
    {
        return _state;
    }

    public void Check(Spot spot)
    {
        if (State.Ongoing != GetState())
        {
            throw new CanNotCheckAfterAWinException();
        }

        if (_board[spot.Y, spot.X] != (char)Symbols.Empty)
        {
            throw new CanNotCheckTheSameSpotException();
        }

        var emptyCount = GetEmptySpotsCount();

        _board[spot.Y, spot.X] = (char)(emptyCount % 2 == 0 ? Symbols.Nought : Symbols.Cross);

        CheckWinner();
    }

    public Spot[] GetEmptySpots()
    {
        var emptySpots = new List<Spot>();

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (_board[y, x] == (char)Symbols.Empty)
                {
                    emptySpots.Add(new Spot(x, y));
                }
            }
        }

        return emptySpots.ToArray();
    }

    public int GetEmptySpotsCount()
    {
        var emptyCount = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (_board[i, j] == '_')
                {
                    emptyCount++;
                }
                
            }
        }

        return emptyCount;
    }

    private void CheckWinner()
    {
        var winner = CheckColumns() ?? CheckRows() ?? CheckDiagonals();

        switch (winner)
        {
            case 'x':
                _state = State.WinX;
                break;
            case 'o':
                _state = State.WinO;
                break;
            case null when 0 == GetEmptySpotsCount():
                _state = State.Draw;
                break;
        }
    }

    private char? CheckDiagonals()
    {
        if (_board[0, 0] != (char)Symbols.Empty && _board[0, 0] == _board[1, 1] && _board[0, 0] == _board[2, 2])
        {
            return _board[0, 0];
        }

        if (_board[0, 2] != (char)Symbols.Empty && _board[0, 2] == _board[1, 1] && _board[0, 2] == _board[2, 0])
        {
            return _board[0, 2];
        }

        return null;
    }

    private char? CheckRows()
    {
        if (_board[0, 0] != (char)Symbols.Empty && _board[0, 0] == _board[0, 1] && _board[0, 0] == _board[0, 2])
        {
            return _board[0, 0];
        }

        if (_board[1, 0] != (char)Symbols.Empty && _board[1, 0] == _board[1, 1] && _board[1, 0] == _board[1, 2])
        {
            return _board[1, 0];
        }

        if (_board[2, 0] != (char)Symbols.Empty && _board[2, 0] == _board[2, 1] && _board[2, 0] == _board[2, 2])
        {
            return _board[2, 0];
        }

        return null;
    }

    private char? CheckColumns()
    {
        if (_board[0, 0] != (char)Symbols.Empty && _board[0, 0] == _board[1, 0] && _board[0, 0] == _board[2, 0])
        {
            return _board[0, 0];
        }

        if (_board[0, 1] != (char)Symbols.Empty && _board[0, 1] == _board[1, 1] && _board[0, 1] == _board[2, 1])
        {
            return _board[0, 1];
        }

        if (_board[0, 2] != (char)Symbols.Empty && _board[0, 2] == _board[1, 2] && _board[0, 2] == _board[2, 2])
        {
            return _board[0, 2];
        }

        return null;
    }

    public override string ToString()
    {
        return $"{_board[0, 0]} {_board[0, 1]} {_board[0, 2]}" + Environment.NewLine +
               $"{_board[1, 0]} {_board[1, 1]} {_board[1, 2]}" + Environment.NewLine +
               $"{_board[2, 0]} {_board[2, 1]} {_board[2, 2]}";
    }
}

public struct Spot
{
    public Spot(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }

    public override string ToString()
    {
        return $"Spot X:{X}, Y:{Y};";
    }
}

public enum State
{
    Ongoing,
    WinX,
    WinO,
    Draw
}

internal enum Symbols
{
    Empty = '_',
    Cross = 'x',
    Nought = 'o'
}