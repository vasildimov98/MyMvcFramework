﻿namespace MyWebFramework.HTTP.Models
{
    public class Cookie
    {
        public Cookie(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public Cookie(string cookieAsString)
        {
            var nameValuePair = cookieAsString.Split('=', 2);

            this.Name = nameValuePair[0];
            this.Value = nameValuePair[1];
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Name}={Value}";
        }
    }
}