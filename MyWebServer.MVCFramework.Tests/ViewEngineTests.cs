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
        public void Return_Expected_Clean_HTML_Result(string fileName)
        {
            // Arrange
            var model = new TestTemplateModel
            {
                Name = "Djaro",
                Price = 2020.20M,
                DateOfBirth = new DateTime(2024, 2, 7)
            };

            var viewEngine = new MyViewEngine();
            var template = File.ReadAllText($"Templates/{fileName}.Template.html");
            var expected = File.ReadAllText($"Templates/{fileName}.Result.html");

            // Act
            var result = viewEngine.GenerateHTML(template, model);

            // Assert
            Assert.Equal(expected, result);
        }
    }

    internal class TestTemplateModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}