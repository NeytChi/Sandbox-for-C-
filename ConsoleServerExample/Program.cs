using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleServerExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var server = new Server();
            server.SetupAsync();
            Console.Read();
        }
    }
    public class Server
    {
        private IPAddress LocalIpAddress;
        private IPEndPoint LocalIpEndPoint;
        private string Host = "localhost";
        private int Port = 8000;

        public Server()
        {
            // var hostName = Dns.GetHostName();
            // var localhost = Dns.GetHostEntry(hostName);
            // LocalIpAddress = localhost.AddressList[0];
            Console.WriteLine($"Get {Host} address for server...");
            IPAddress address = Dns.GetHostAddresses(Host).First(x => x.AddressFamily == AddressFamily.InterNetwork);
            LocalIpEndPoint = new IPEndPoint(address, Port);
        }
        public async void SetupAsync()
        {
            using (var listener = new Socket(LocalIpEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                listener.Bind(LocalIpEndPoint);
                listener.Listen(100);

                Console.WriteLine($"Listen {Port} by TCP socket...");
                var handler = await listener.AcceptAsync();
                while (true)
                {
                    var buffer = new byte[1024];
                    var received = handler.Receive(buffer, SocketFlags.None);
                    var response = Encoding.UTF8.GetString(buffer, 0, received);

                    var eom = "<|EOM|>";
                    if (response.IndexOf(eom) > -1 /* is end of message */)
                    {
                        Console.WriteLine(
                            $"Socket server received message: \"{response.Replace(eom, "")}\"");

                        var ackMessage = "<|ACK|>";
                        var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
                        handler.Send(echoBytes, 0);
                        Console.WriteLine(
                            $"Socket server sent acknowledgment: \"{ackMessage}\"");
                        break;
                    }
                }
            }
        }
    }
}
