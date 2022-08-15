namespace GameRules;

public class GameSession
{
    private readonly IPlayer _firstPlayer;
    private readonly IPlayer _secondPlayer;
    private IPlayer _nextPlayer;
    private Game _game;

    public GameSession(IPlayer firstPlayer, IPlayer secondPlayer)
    {
        _firstPlayer = firstPlayer;
        _secondPlayer = secondPlayer;
        _nextPlayer = firstPlayer;
        _game = new Game();
    }

    public void MakeTurn()
    {
        var spot = _nextPlayer.ChooseSpot(_game);
        if (null == spot)
        {
            return;
        }
        
        _game.Check((Spot)spot);
        _nextPlayer = _nextPlayer == _firstPlayer
            ? _secondPlayer
            : _firstPlayer;
    }

    public State GetState()
    {
        return _game.GetState();
    }

    public char[,] GetBoard()
    {
        return _game.GetBoard();
    }
    
    public Game GetGame()
    {
        return _game;
    }
}