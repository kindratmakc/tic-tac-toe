namespace GameRules;

public interface IPlayer
{
    public Spot? ChooseSpot(IBoardProvider board);
}