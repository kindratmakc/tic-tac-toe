namespace GameRules;

public class RealPlayer : IPlayer
{
    private readonly IInput _input;

    public RealPlayer(IInput input)
    {
        _input = input;
    }

    public Spot? ChooseSpot(IBoardProvider board)
    {
        return _input.GetSpot();
    }
}