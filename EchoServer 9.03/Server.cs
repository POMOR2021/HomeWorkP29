using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        var ip = IPAddress.Parse("127.0.0.1");
        var port = 8888;

        var srv = new TcpListener(ip, port);

        try
        {
            srv.Start();
            Console.WriteLine($"Сервер запущен {ip}:{port}");

            while (true)
            {
                using (var client = srv.AcceptTcpClient())
                using (var stream = client.GetStream())
                {
                    Console.WriteLine($"Клиент подключен");

                    var buf = new byte[1024];
                    int cnt;

                    while ((cnt = stream.Read(buf, 0, buf.Length)) > 0)
                    {
                        var msg = Encoding.UTF8.GetString(buf, 0, cnt);
                        Console.WriteLine($"Получено: {msg}");

                        var res = Encoding.UTF8.GetBytes(msg);
                        stream.Write(res, 0, res.Length);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        finally
        {
            srv.Stop();
        }
    }
}