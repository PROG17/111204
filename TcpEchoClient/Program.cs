using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TcpEchoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ange host:");
            var host = Console.ReadLine();
            Console.WriteLine("Ange port:");
            var port = int.Parse(Console.ReadLine());

            using (var client = new TcpClient(host, port))
            using (var networkStream = client.GetStream())
            using (StreamReader reader = new StreamReader(networkStream, Encoding.UTF8))
            using (var writer = new StreamWriter(networkStream, Encoding.UTF8) { AutoFlush = true })
            {
                Console.WriteLine($"Ansluten till {client.Client.RemoteEndPoint}");
                while (client.Connected)
                {
                    Console.WriteLine("Ange text att skicka: (Skriv QUIT för att avsluta)");
                    var text = Console.ReadLine();

                    if (text == "QUIT") break;

                    // Skicka text
                    writer.WriteLine(text);

                    if (!client.Connected) break;

                    // Läs minst en rad
                    do
                    {
                        var line = reader.ReadLine();
                        Console.WriteLine($"Svar: {line}");

                    } while (networkStream.DataAvailable);

                };

            }


        }
    }
}
