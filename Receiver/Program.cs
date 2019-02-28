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
            Console.WriteLine("Receiver started");
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
                var signal = (TrueTradeSignal)JsonConvert.DeserializeObject(jsonMessage, typeof(TrueTradeSignal));

                var binarySignal = new BinaryTradeSignal();
                binarySignal.price = 100;
                binarySignal.parameters = new BinaryTradeSignalParameters
                {
                    amount = 777,
                    basis = "payout",
                    contract_type = signal.callput,
                    currency = "USD",
                    date_start = DateTime.Now.Ticks,
                    date_expiry = signal.date_expiry,
                    duration = signal.tfdigi,
                    duration_unit = signal.tfdur
                    //symbol = ?
                };
                //binarySignal.passthrough = ?
                //binarySignal.req_id = ?

                jsonMessage = JsonConvert.SerializeObject(binarySignal);

                // create a data transfer object which will contain fields specified at goodle drive document
                // populate transfer object fields based on values from ionbound message and rules specified at document
                //      if there is no rule - assume this field value as constant equal to default value specified at the document
                // create a json representation of data transfer object
                // print json representation to console
                Console.WriteLine($"Parsed message {signal.notify_type} {signal.date_expiry}");
            }
        }
    }
}
