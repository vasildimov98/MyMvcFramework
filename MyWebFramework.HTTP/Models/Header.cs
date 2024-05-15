namespace MyWebFramework.HTTP.Models
{
    public class Header
    {
        public Header(string line)
        {
            var nameValuePair = line.Split(": ", 2);

            this.Name = nameValuePair[0];
            this.Value = nameValuePair[1];
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{this.Name}: {this.Value}";
        }
    }
}