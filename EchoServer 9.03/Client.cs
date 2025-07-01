using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        var ip = "127.0.0.1";
        var port = 8888;

        try
        {
            using (var cli = new TcpClient(ip, port))
            using (var stream = cli.GetStream())
            {
                Console.WriteLine($"Подключено к {ip}:{port}");
                Console.WriteLine("Вводите текст (exit для выхода):");

                while (true)
                {
                    Console.Write("> ");
                    var msg = Console.ReadLine();

                    if (msg.ToLower() == "exit")
                        break;

                    var data = Encoding.UTF8.GetBytes(msg);
                    stream.Write(data, 0, data.Length);

                    var buf = new byte[1024];
                    var cnt = stream.Read(buf, 0, buf.Length);
                    var res = Encoding.UTF8.GetString(buf, 0, cnt);
                    Console.WriteLine($"Ответ: {res}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}