using System;
using System.IO;
using System.Threading;

public class Bank
{
    private int _money;
    private string _name;
    private int _percent;
    private readonly string _logFilePath;
    private readonly object _lockObject = new object();

    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            LogChanges();
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            LogChanges();
        }
    }

    public int Percent
    {
        get => _percent;
        set
        {
            _percent = value;
            LogChanges();
        }
    }

    public Bank(int money, string name, int percent, string logFilePath = "bank_log.txt")
    {
        _money = money;
        _name = name;
        _percent = percent;
        _logFilePath = logFilePath;
    }

    private void LogChanges()
    {
        Thread thread = new Thread(() =>
        {
            try
            {
                lock (_lockObject)
                {
                    string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - " +
                                    $"Money: {_money}, Name: {_name}, Percent: {_percent}%";

                    File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в лог: {ex.Message}");
            }
        });

        thread.IsBackground = true;
        thread.Start();
    }

    public override string ToString()
    {
        return $"Bank: {_name}, Money: {_money}, Percent: {_percent}%";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Bank bank = new Bank(10000, "MyBank", 5);

        Console.WriteLine("Исходное состояние:");
        Console.WriteLine(bank);

        Console.WriteLine("\nИзменяем свойства...");
        bank.Money = 15000;
        bank.Name = "SuperBank";
        bank.Percent = 7;

        Console.WriteLine("\nНовое состояние:");
        Console.WriteLine(bank);

        Console.WriteLine("\nПроверьте файл bank_log.txt на диске для просмотра журнала изменений.");
        Console.ReadKey();
    }
}