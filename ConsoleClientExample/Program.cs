using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClientExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();
            client.Setup();
            Console.Read();
        }
    }
    public class Client
    {
        private IPAddress LocalIpAddress;
        private IPEndPoint LocalIpEndPoint;
        private string Host = "localhost";
        private int Port = 8000;
        public Client()
        {
            // var hostName = Dns.GetHostName();
            // var localhost = Dns.GetHostEntry(hostName);
            Console.WriteLine($"Get {Host} address for client...");
            IPAddress address = Dns.GetHostAddresses(Host).First(x => x.AddressFamily == AddressFamily.InterNetwork);
            LocalIpEndPoint = new IPEndPoint(address, Port);
        }
        public async void Setup()
        {
            using (var client = new Socket(LocalIpEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                await client.ConnectAsync(LocalIpEndPoint);
                while (true)
                {
                    var message = "Hi friends 👋!<|EOM|>";
                    var messageBytes = Encoding.UTF8.GetBytes(message);
                    var check = client.Send(messageBytes, SocketFlags.None);
                    Console.WriteLine($"Socket client sent message: \"{message}\"");

                    var buffer = new byte[1_024];
                    var received = client.Receive(buffer, SocketFlags.None);
                    var response = Encoding.UTF8.GetString(buffer, 0, received);
                    if (response == "<|ACK|>")
                    {
                        Console.WriteLine(
                            $"Socket client received acknowledgment: \"{response}\"");
                        break;
                    }
                }

                client.Shutdown(SocketShutdown.Both);
            }
        }
    }
}
