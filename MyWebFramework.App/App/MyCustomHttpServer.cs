using MyWebFramework.App.App;
using MyWebServer.Client;
using System.Text;

internal class MyCustomHttpServer(IConnector connector, IUserInteractor userInteractor, IBuilder builder, IHttpEngine http)
{
    private readonly IList<Tweet> tweets = [];
    private const string NEW_LINE = "\r\n";

    internal void Start()
    {
        connector.StartListening();
       

        try
        {
            while (true)
            {
                using var client = connector.AcceptTcpClient();
                using var stream = client.GetStream();
                var request = http.ProcessRequest(stream);

                var path = ExtractPath(request);

                switch (path)
                {
                    case "/":
                        http.ProcessResponse(stream, CreateHomeResponse());
                        break;
                    case "/tweet":
                        var tweet = ExtractContent(request);
                        tweets.Add(tweet);
                        http.ProcessResponse(stream, CreateAllPost());
                        break;
                    case "empty":
                        continue;
                    case "/favicon.ico":
                        continue;
                    default:
                        http.ProcessResponse(stream, CreateHomeResponse());
                        break;
                }

            }
        }
        catch (Exception ex)
        {
            userInteractor.ShowMessage($"Error: {ex.Message}");
        }
    }

    private string CreateAllPost()
    {
        var postsBuilder = new StringBuilder();
        postsBuilder.Append("<ul>" + NEW_LINE);
        foreach (var tweet in tweets) 
        {
            postsBuilder.Append("<li>" + NEW_LINE);
            postsBuilder.Append($"From: {tweet.From}; Content: {tweet.Content}");
            postsBuilder.Append("</li>" + NEW_LINE);
        }
        postsBuilder.Append("</ul>" + NEW_LINE);

        var posts = postsBuilder.ToString();

        return this.GetOkResponse()
          .AppendLine($"Content-Length: {posts.Length}")
          .AppendNewLine()
          .AppendLine(posts)
          .GetResponse();
    }

    private string CreateHomeResponse()
    {
        var home = File.ReadAllText("./root/home.html");

        return this.GetOkResponse()
          .AppendLine($"Content-Length: {home.Length}")
          .AppendNewLine()
          .AppendLine(home)
          .GetResponse();
    }

    private ResponseBuilder GetOkResponse()
    {
        return builder
          .AppendLine("HTTP/1.1 200 OK")
          .AppendLine("Server: Vasko Server 2024")
          .AppendLine("Content-Type: text/html; charset=utf-8");
    }

    private static string ExtractPath(string request)
    {
        try
        {
            var path = request.Split(NEW_LINE)[0].Split(" ")[1];

            return path;
        }
        catch (Exception)
        {
            return "/";
        }
    }

    private Tweet ExtractContent(string request)
    {
        var content = request
            .Split(NEW_LINE)
            .Last()
            .Trim()
            .Split("&")
            .Select(x => x.Split('=')[1])
            .ToArray();

        var tweet = new Tweet
        {
            From = content[0],
            Content = content[1],
        };

        return tweet;
    }
}

internal class Tweet
{
    public string? From { get; set; }
    public string? Content { get; set; }
}