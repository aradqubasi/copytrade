using Newtonsoft.Json;
using System;
using System.IO;
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

                Console.WriteLine($"Incoming message {Encoding.ASCII.GetString(data)}");

                string jsonMessage;
                using (var stream = new MemoryStream(data))
                {
                    jsonMessage = new StreamReader(stream).ReadToEnd();
                }
                var signal = (TradeSignal)JsonConvert.DeserializeObject(jsonMessage, typeof(TradeSignal));


                Console.WriteLine($"Parsed message {signal.buyOrSell} {signal.amount}");
            }
        }
    }
}
