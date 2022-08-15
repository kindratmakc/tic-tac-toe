namespace GameRules;

public interface IBoardProvider : ICloneable
{
    public void Check(Spot spot);

    public Spot[] GetEmptySpots();

    public State GetState();

    public int GetEmptySpotsCount();
}