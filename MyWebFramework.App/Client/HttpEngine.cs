using System.Net.Sockets;
using System.Text;

namespace MyWebServer.Client
{
    internal class HttpEngine : IHttpEngine
    {
        private readonly IUserInteractor userInteractor;

        public HttpEngine(IUserInteractor userInteractor)
        {
            this.userInteractor = userInteractor;
        }

        public string ProcessRequest(NetworkStream stream)
        {
            try
            {
                var path = ReadRequest(stream);

                return path;
            }
            catch 
            {
                throw;
            }
        }

        private string ReadRequest(NetworkStream stream)
        {
            var request = new StringBuilder();

            while (true)
            {
                var buffer = new byte[1024 * 4];

                var numberOfReadData = stream.Read(buffer, 0, buffer.Length);

                if (numberOfReadData == 0)
                {
                    return "empty";
                };

                var data = Encoding.UTF8.GetString(buffer, 0, numberOfReadData);

                request.Append(data);

                if (numberOfReadData < buffer.Length) break;
            }

            var requestString = request.ToString();

            userInteractor.ShowMessage("Received request:");
            userInteractor.AddNewline();
            userInteractor.ShowMessage(requestString);
            userInteractor.ShowMessage(new string('=', 100));

            return requestString;
        }

        public void ProcessResponse(NetworkStream stream, string response)
        {
            try
            {
                userInteractor.ShowMessage("Sending response:");
                userInteractor.AddNewline();
                userInteractor.ShowMessage(response);
                userInteractor.ShowMessage(new string('=', 100));

                stream.Write(Encoding.UTF8.GetBytes(response));
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}