using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = 40000;
            IPEndPoint anyoneThroughPort = new IPEndPoint(IPAddress.Loopback, port);
            var client = new UdpClient(anyoneThroughPort);
            while (true)
            {
                var data = client.Receive(ref anyoneThroughPort);
                Console.WriteLine(Encoding.ASCII.GetString(data));
            }
        }
    }
}
