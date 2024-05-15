using MyWebFramework.Common.Constant;
using MyWebFramework.Common.Interactors;
using MyWebFramework.HTTP.Delegates;
using MyWebFramework.HTTP.Models;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MyWebFramework.HTTP.Servers
{
    public class HttpServer(IUserInteractor userInteractor) : IHttpServer
    {
        private readonly IDictionary<string, HttpHandler?> routesMap
            = new Dictionary<string, HttpHandler?>();

        public void AddRoute(string url, HttpHandler handler)
        {
            if (!routesMap.ContainsKey(url))
            {
                routesMap.Add(url, null);
            }

            routesMap[url] = handler;
        }

        public async Task StartListeningAsync(int port)
        {
            using var tcpListener = new TcpListener(IPAddress.Loopback, port);

            tcpListener.Start();

            userInteractor.ShowMessage($"Start listening on port: {port}");
            userInteractor.AddNewline();

            while (true)
            {
                using var client = await tcpListener.AcceptTcpClientAsync();
                _ = ProcessClientAsync(client);
            }
        }

        private static async Task ProcessClientAsync(TcpClient client)
        {
            using var stream = client.GetStream();

            var request = await GetRequestAsync(stream);

            var httpRequest = new HttpRequest(request);

            Console.WriteLine(request);

            Console.WriteLine(new string('=', 100));
        }

        private static async Task<string> GetRequestAsync(NetworkStream stream)
        {
            var position = 0;
            var data = new List<byte>();
            while (true)
            {
                var buffer = new byte[HttpConstants.BUFFER_SIZE];

                var countOfDataRead = await stream.ReadAsync(buffer.AsMemory(position, buffer.Length));
                position += countOfDataRead;

                if (countOfDataRead < buffer.Length) 
                {
                    var partialBuffer = new byte[countOfDataRead];
                    Array.Copy(buffer, partialBuffer, countOfDataRead);
                    data.AddRange(partialBuffer);
                    break;
                }

                data.AddRange(buffer);
            }

            var request = Encoding.UTF8.GetString(data.ToArray());

            return request;
        }
    }
}
