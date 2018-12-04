using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TcpClient1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            TcpClient client = null;
            NetworkStream networkStream = null;
            StreamReader reader = null;
            StreamWriter writer = null;

            try
            {
                client = new TcpClient("www.caribou.se", 80); // Antingen try-finally eller using-statement.

                networkStream = client.GetStream();

                reader = new StreamReader(networkStream);
                writer = new StreamWriter(networkStream);

                writer.WriteLine("GET / HTTP/1.1");
                writer.WriteLine("Host: www.caribou.se");
                writer.WriteLine();
                writer.Flush();

                var result = reader.ReadToEnd();

                Console.WriteLine(result);

            }
            finally
            {
                networkStream.Close();
                client.Close();
            }



        }
    }
}
