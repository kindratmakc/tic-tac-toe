namespace GameRules;

public class RandomAIPlayer : IPlayer
{
    private readonly Random _random = new();

    public Spot? ChooseSpot(IBoardProvider board)
    {
        var emptySpots = board.GetEmptySpots().ToList();

        return emptySpots[_random.Next(emptySpots.Count)];
    }
}