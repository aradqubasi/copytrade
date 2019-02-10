using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Copytrade
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            IPEndPoint Sergiicalledtogod = new IPEndPoint(IPAddress.Any, 40000);
            Socket WinSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            WinSocket.Bind(Sergiicalledtogod);

            Console.Write("Waiting for client");
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);
            byte[] data = new byte[1024];
            int recv = WinSocket.ReceiveFrom(data, ref Remote);
            Console.WriteLine("Message received from {0}:", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
        }
    }
}
