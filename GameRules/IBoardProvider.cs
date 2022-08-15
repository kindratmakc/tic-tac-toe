namespace GameRules;

public interface IBoardProvider : ICloneable
{
    public char[,] GetBoard();

    public void Check(Spot spot);

    public IEnumerable<Spot> GetEmptySpots();

    public State GetState();

    public int GetEmptySpotsCount();
}