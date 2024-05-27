using MyWebServer.MVCFramework.ViewEngine;

namespace MyWebServer.MVCFramework.Tests
{
    public class GenerateHTML_Should
    {
        [Theory]
        [InlineData("CleanHtml")]
        [InlineData("Foreach")]
        [InlineData("IfElseFor")]
        [InlineData("TemplateWithModel")]
        public void Return_Expected_HTML_Result(string fileName)
        {
            // Arrange
            var model = new TestTemplateModel
            {
                Name = "Djaro",
                Price = 2020.20M,
                DateOfBirth = new DateTime(2024, 2, 7)
            };

            var viewEngine = new MyViewEngine();
            var template = File
                .ReadAllText($"Templates/{fileName}.Template.html");

            var expected = File
                .ReadAllText($"Templates/{fileName}.Result.html");

            if (!expected.Contains("\r\n"))
            {
                expected = expected.Replace("\n", "\r\n");
            }

            // Act
            var result = viewEngine.GenerateHTML(template, model);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Work_Correctly_With_Generics()
        {
            // Arrange
            var viewEngine = new MyViewEngine();
            var template = @"@foreach(var num in Model)
{
<span>@num</span>
}";
            var model = new List<int>
            {
                1,
                2,
                3,
            };
            var expected = @"<span>1</span>
<span>2</span>
<span>3</span>";

            // Act 
            var actual = viewEngine.GenerateHTML(template, model);

            // Assert
            Assert.Equal(expected, actual);
        }
    }

    public class TestTemplateModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}