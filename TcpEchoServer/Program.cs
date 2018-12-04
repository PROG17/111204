using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

            try
            {
                try
                {
                    listener = new TcpListener(IPAddress.Any, listenOnPort);
                    listener.Start();
                    Console.WriteLine($"Starts listening on port: {listenOnPort}");
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("Misslyckades att öppna socket. Troligtvis upptagen.");
                    Environment.Exit(1);
                }
                
                

                while (true)
                {
                    // listener.Pending() Kolla om det finns någon på kö
                    client = listener.AcceptTcpClient();
                    networkStream = client.GetStream();
                    Console.WriteLine($"Ny TcpClient...");

                    byte[] receiveBuffer = new byte[32];
                    int receivedBytes;
                    int totalBytes = 0;

                    while ((receivedBytes = networkStream.Read(receiveBuffer, 0, receiveBuffer.Length)) > 0)
                    {
                        networkStream.Write(receiveBuffer, 0, receivedBytes);
                        Console.WriteLine("Mottagit:"+ Encoding.ASCII.GetString(receiveBuffer,0, receivedBytes));
                        totalBytes += receivedBytes;
                    }

                    Console.WriteLine($"Done. Echoed {totalBytes} bytes.");
                    networkStream.Close();
                    client.Close();


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
