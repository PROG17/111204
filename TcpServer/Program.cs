using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServer
{
    class Program
    {
        static TcpListener listener;

        static void StartListen(int port)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                Console.WriteLine($"Starts listening on port: {port}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Misslyckades att öppna socket. Troligtvis upptagen.");
                Environment.Exit(1);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till servern");
            Console.WriteLine("Ange port att lyssna på:");
            var port = int.Parse(Console.ReadLine());

            StartListen(port);

            while (true)
            {
                Console.WriteLine("Väntar på att någon ska ansluta sig...");

                using (var client = listener.AcceptTcpClient())
                using (var networkStream = client.GetStream())
                using (StreamReader reader = new StreamReader(networkStream, Encoding.UTF8))
                using (var writer = new StreamWriter(networkStream, Encoding.UTF8) { AutoFlush = true })
                {
                    Console.WriteLine($"Klient har anslutit sig {client.Client.RemoteEndPoint}!");

                    while (client.Connected)
                    {
                        var command = reader.ReadLine();
                        Console.WriteLine($"Mottaget: {command}");

                        if (string.Equals(command, "EXIT", StringComparison.InvariantCultureIgnoreCase)){
                            writer.WriteLine("BYE BYE");
                            break;
                        }

                        if (string.Equals(command, "DATE", StringComparison.InvariantCultureIgnoreCase))
                        {
                            writer.WriteLine(DateTime.UtcNow.ToString("o"));
                            break;
                        }

                        writer.WriteLine($"UNKNOWN COMMAND: {command}");
                    }
                }

            }

        }
    }
}
