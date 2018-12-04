using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpEchoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("UdpEchoClient");

            Console.WriteLine("Ange host:");
            var host = Console.ReadLine();
            Console.WriteLine("Ange port:");
            var port = int.Parse(Console.ReadLine());

            var client = new UdpClient();

            while (true)
            {
                Console.WriteLine("Ange text att skicka:");
                var text = Console.ReadLine();

                var data = Encoding.UTF8.GetBytes(text);

                client.Send(data, data.Length, host, port);
            }
        }
    }
}
