using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpEchoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("UdpEchoServer");

            Console.WriteLine("Ange port att lyssna på:");
            var port = int.Parse(Console.ReadLine());

            var remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);

            using (var client = new UdpClient(port))
            {
                while (true)
                {
                    var data = client.Receive(ref remoteEndpoint);
                    var text = Encoding.UTF8.GetString(data);
                    Console.WriteLine($"Endpoint: {remoteEndpoint} - {text}");
                }
            }
        }
    }
}
