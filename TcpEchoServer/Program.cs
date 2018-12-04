using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TcpEchoServer
{
    class Program
    {
        static int listenOnPort = 5000;

        static void Main(string[] args)
        {
            TcpListener listener = null;
            TcpClient client = null;
            NetworkStream networkStream = null;
            StreamReader reader = null;
            StreamWriter writer = null;

            try
            {
                listener = new TcpListener(IPAddress.Any, listenOnPort);
                listener.Start();
                Console.WriteLine($"Starts listening on port: {listenOnPort}");

                while (true)
                {
                    // listener.Pending() Kolla om det finns någon på kö
                    client = listener.AcceptTcpClient();

                    Console.WriteLine($"Ny TcpClient...");

                    networkStream = client.GetStream();

                    reader = new StreamReader(networkStream);
                    writer = new StreamWriter(networkStream);

                    while (true)
                    {
                        var data = reader.ReadLine();
                        writer.WriteLine("ECHO: " + data);
                        writer.Flush();
                        Console.WriteLine("ECHO: " + data);
                    }



                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Det gick åt skogen! {ex.Message}");
            }
            finally
            {   
            }

            
        }
    }
}
