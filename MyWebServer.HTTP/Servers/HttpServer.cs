using MyWebFramework.Common.Constant;
using MyWebFramework.Common.Interactors;
using MyWebFramework.HTTP.Delegates;
using MyWebFramework.HTTP.Models;
using MyWebFramework.Common.Builder;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace MyWebFramework.HTTP.Servers
{
    public class HttpServer(IUserInteractor userInteractor) : IHttpServer
    {
        private readonly IDictionary<string, HttpHandler> routesTable
            = new Dictionary<string, HttpHandler>();
        private readonly IUserInteractor userInteractor = userInteractor;

        public void AddRoute(string url, HttpHandler handler)
        {
            if (!routesTable.ContainsKey(url))
            {
                routesTable.Add(url, handler);
                return;
            }

            routesTable[url] = handler;
        }

        public async Task StartListeningAsync(int port)
        {
            var tcpListener = new TcpListener(IPAddress.Loopback, port);

            tcpListener.Start();

            userInteractor.ShowMessage($"Start listening on port: {port}");
            userInteractor.AddNewline();

            while (true)
            {
                var client = await tcpListener.AcceptTcpClientAsync();

                try
                {
                    _ = ProcessClientAsync(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task ProcessClientAsync(TcpClient client)
        {
            using var stream = client.GetStream();

            var request = await GetRequestAsync(stream);

            var httpRequest = new HttpRequest(request);

            userInteractor.ShowMessage(request);

            userInteractor.ShowMessage(new string('=', 100));

            var response = await SendReponseAsync(stream, httpRequest);

            userInteractor.ShowMessage(response);

            userInteractor.ShowMessage(new string('=', 100));

            client.Close();
        }

        private async Task<string> SendReponseAsync(NetworkStream stream, HttpRequest request)
        {
            HttpResponse httpResponse;
            if (!routesTable.TryGetValue(request.Path, out HttpHandler? value))
            {
                var notFoundHtml = "<h1>Page Not Found</h1>";
                var notFoundByte = Encoding.UTF8.GetBytes(notFoundHtml);
                httpResponse = new HttpResponse("text/html; charset=utf-8", notFoundByte, StatusCode.NotFound);
            } 
            else
            {
                var action = value;

                httpResponse = action(request);

                httpResponse.Headers.Add(new Header("Server", "VaskoServer 2024"));

                //httpResponse.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
                //{
                //    MaxAge = 4 * 24 * 60 * 60 * 60,
                //    HttpOnly = true
                //});
            }

            var responseAsString = httpResponse.ToString();

            var byteResponse = Encoding.UTF8.GetBytes(responseAsString);

            await stream.WriteAsync(byteResponse.AsMemory(0, byteResponse.Length));
            await stream.WriteAsync(httpResponse.Body.AsMemory(0, httpResponse.Body.Length));

            return responseAsString;
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
