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
                var signal = new TradeSignal();
                var haveTradeType = false;
                var haveAmount = false;
                using (var reader = new JsonTextReader(new StreamReader(new MemoryStream(data))))
                {
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.String)
                        {
                            if ((string)reader.Value == "buy")
                            {
                                signal.buyOrSell = TradeType.Buy;
                                haveTradeType = true;
                            }
                            else if ((string)reader.Value == "sell")
                            {
                                signal.buyOrSell = TradeType.Sell;
                                haveTradeType = true;
                            }
                            else
                            {
                                Console.WriteLine($"Unexpected buy_or_sell value {reader.Value}");
                                break;
                            }
                        }
                        else if (reader.TokenType == JsonToken.Integer)
                        {
                            signal.amount = (long)reader.Value;
                            haveAmount = true;
                        }
                    }
                }
                if (haveTradeType && haveAmount)
                {
                    Console.WriteLine($"Incoming message was parsed successfully {signal.buyOrSell} {signal.amount}");
                }
                else
                {
                    Console.WriteLine($"Incoming message {Encoding.ASCII.GetString(data)}");
                }
                //Console.WriteLine(Encoding.ASCII.GetString(data));
            }
        }
    }
}
