namespace MyWebServer.HTTP.Models
{
    public class Cookie
    {
        public Cookie(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public Cookie(string cookieAsString)
        {
            var nameValuePair = cookieAsString.Split('=', 2);

            Name = nameValuePair[0];
            Value = nameValuePair[1];
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Name}={Value}";
        }
    }
}