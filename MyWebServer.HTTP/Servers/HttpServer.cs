using MyWebFramework.Common.Interactors;
using MyWebServer.Common.Constants;
using MyWebServer.HTTP.Delegates;
using MyWebServer.HTTP.Models;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MyWebServer.HTTP.Servers
{
    public class HttpServer(IList<Route> routes, IUserInteractor userInteractor) : IHttpServer
    {
        private readonly IList<Route> routes = routes;
        private readonly IUserInteractor userInteractor = userInteractor;

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

            userInteractor.ShowMessage($"Request: {httpRequest.Method} {httpRequest.Path} => {httpRequest.Headers.Count()}");

            userInteractor.ShowMessage(new string('=', 100));

            var httpResponse = await SendReponseAsync(stream, httpRequest);

            userInteractor.ShowMessage($"Response: {httpResponse.StatusCode} => {httpRequest.Headers.Count()}");

            userInteractor.ShowMessage(new string('=', 100));

            client.Close();
        }

        private async Task<HttpResponse> SendReponseAsync(NetworkStream stream, HttpRequest request)
        {
            HttpResponse httpResponse;
            var route = routes
                .FirstOrDefault(x => 
                    string.Compare(x.Url, request.Path, true) == 0 &&
                    x.Method == request.Method);

            if (route == null)
            {
                var notFoundHtml = "<h1>Page Not Found</h1>";
                var notFoundByte = Encoding.UTF8.GetBytes(notFoundHtml);
                httpResponse = new HttpResponse("text/html; charset=utf-8", notFoundByte, StatusCode.NotFound);
            }
            else
            {
                httpResponse = route.Handler(request);
            }

            httpResponse.Headers.Add(new Header("Server", "VaskoServer 2024"));

            httpResponse.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
            {
                MaxAge = 4 * 24 * 60 * 60 * 60,
                HttpOnly = true
            });

            var byteResponse = Encoding.UTF8.GetBytes(httpResponse.ToString());

            await stream.WriteAsync(byteResponse.AsMemory(0, byteResponse.Length));
            await stream.WriteAsync(httpResponse.Body.AsMemory(0, httpResponse.Body.Length));

            return httpResponse;
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
