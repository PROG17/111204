using System;
using System.Net; // Innehåller det mesta av nätverksklasserna
using System.Net.NetworkInformation;

namespace DnsAndPing
{
    class Program
    {
        public static bool PingHost(IPAddress nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;

                Console.WriteLine($"Reply from {reply.Address} in {reply.RoundtripTime} ms.");

            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        static void Main(string[] args)
        {
            // DNS
            var hostname = Dns.GetHostName();

            Console.WriteLine($"GetHostName: {hostname}");

            IPHostEntry entry = Dns.GetHostEntry("www.caribou.se");

            Console.WriteLine($"GetHostEntry: {entry.HostName}");
            foreach (IPAddress address in entry.AddressList)
            {
                Console.WriteLine($"  Address: {address}");

                PingHost(address);
            }






        }
    }
}
