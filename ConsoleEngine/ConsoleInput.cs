using GameRules;

namespace ConsoleEngine;

public class ConsoleInput : IInput
{
    public Spot? GetSpot()
    {
        Console.WriteLine("Please, make your choice...");
        
        try
        {
            return MapToSpot(Console.ReadKey().Key);
        }
        catch (ArgumentOutOfRangeException e)
        {
            return GetSpot();
        }
    }

    private static Spot MapToSpot(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.D1:
                return new Spot(0, 0);
            case ConsoleKey.D2:
                return new Spot(1, 0);
            case ConsoleKey.D3:
                return new Spot(2, 0);
            case ConsoleKey.D4:
                return new Spot(0, 1);
            case ConsoleKey.D5:
                return new Spot(1, 1);
            case ConsoleKey.D6:
                return new Spot(2, 1);
            case ConsoleKey.D7:
                return new Spot(0, 2);
            case ConsoleKey.D8:
                return new Spot(1, 2);
            case ConsoleKey.D9:
                return new Spot(2, 2);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}