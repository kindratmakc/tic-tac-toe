namespace GameRules;

public class CanNotCheckTheSameSpotException : Exception
{
    public CanNotCheckTheSameSpotException() : base("Can't check the same spot")
    {
    }
}