using System.Net.Sockets;

namespace MyWebServer.Client
{
    internal interface IHttpEngine
    {
        string ProcessRequest(NetworkStream stream);
        void ProcessResponse(NetworkStream stream, string response);
    }
}