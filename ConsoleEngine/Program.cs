// See https://aka.ms/new-console-template for more information

using ConsoleEngine;
using GameRules;

var session = new GameSession(
    new RealPlayer(new ConsoleInput()),
    new RandomAIPlayer()
);

do
{
    var board = session.GetBoard();
    
    Console.Clear();
    for (var y = 0; y < 3; y++)
    {
        for (var x = 0; x < 3; x++)
        {
            Console.Write(board[y, x]);
        }
        Console.WriteLine();
    }
    
    session.MakeTurn();
} while (session.GetState() == State.Ongoing);

switch (session.GetState())
{
    case State.WinX:
        Console.WriteLine("X win!!!");
        break;
    case State.WinO:
        Console.WriteLine("O win!!!");
        break;
    case State.Draw:
        Console.WriteLine("Draw :(");
        break;
    default:
        throw new ArgumentOutOfRangeException();
}

Console.WriteLine("Press any key to continue...");
Console.ReadKey();