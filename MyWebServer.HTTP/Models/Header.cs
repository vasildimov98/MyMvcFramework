namespace MyWebServer.HTTP.Models
{
    public class Header
    {
        public Header(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public Header(string line)
        {
            var nameValuePair = line.Split(": ", 2);

            Name = nameValuePair[0];
            Value = nameValuePair[1];
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
    }
}