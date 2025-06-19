using System;
using System.Diagnostics;
using System.Threading;

class ReactionGame
{
    private static readonly Random _random = new Random();

    public static void StartGame()
    {
        Console.WriteLine("Игра 'Успел, не успел'");
        Console.WriteLine("----------------------");
        Console.WriteLine("Когда появится сигнал 'НАЖМИ!', быстро нажмите любую клавишу");
        Console.WriteLine("Нажмите Enter, чтобы начать...");
        Console.ReadLine();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Приготовьтесь...");

            int delay = _random.Next(1000, 5000);
            Thread.Sleep(delay);

            Console.Clear();
            Console.WriteLine("НАЖМИ!");

            Stopwatch stopwatch = Stopwatch.StartNew();
            Console.ReadKey(true);
            stopwatch.Stop();

            Console.Clear();
            Console.WriteLine($"Ваше время реакции: {stopwatch.ElapsedMilliseconds} мс");

            Console.WriteLine("\nСыграем еще? (y,n)");
            if (Console.ReadKey().Key != ConsoleKey.Y)
                break;
        }

        Console.WriteLine("\nСпасибо за игру!");
    }
}


class Program
{
    static void Main(string[] args)
    {
        ReactionGame.StartGame();
    }
}