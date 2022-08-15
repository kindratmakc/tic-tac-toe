namespace GameRules;

public class CanNotCheckAfterAWinException : Exception
{
    public CanNotCheckAfterAWinException() : base("Can not check after a win")
    {
    }
}