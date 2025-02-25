using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncWordCount
{
    class Program
    {
        private static long wCount = 0;
        private static CancellationTokenSource ct = new CancellationTokenSource();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу:");
            string filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файла нет");
                return;
            }
            Task reportingTask = Task.Run(() => ReportWordCount(ct.Token));
            try
            {
                await CountWordsAsync(filePath, ct.Token);

                Console.WriteLine($"Подсчет завершен. Всего слов: {wordCount}");
            }
            finally
            {
                ct.Cancel(); 
                await reportingTask;
            }

            Console.WriteLine("Нажмите любую клавишу для выхода.");
            Console.ReadKey();
        }

        static async Task CountWordsAsync(string filePath, CancellationToken cancellationToken)
        {
            const int bufferSize = 4096; 
            byte[] buffer = new byte[bufferSize];

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, useAsync: true))
            using (StreamReader reader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    cancellationToken.ThrowIfCancellationRequested(); 
                    string[] words = line.Split(new char[] { ' ', '\t', '\r', '\n', ',', '.', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries); 
                    Interlocked.Add(ref wCount, words.Length); 
                }
            }
        }

        static async Task ReportWordCount(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine($"Данное количество слов: {wCount}");
                try
                {
                    await Task.Delay(1000, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
        }
    }
}
