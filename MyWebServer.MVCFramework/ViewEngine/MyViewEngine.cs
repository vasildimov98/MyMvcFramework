
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace MyWebServer.MVCFramework.ViewEngine
{
    public class MyViewEngine : IViewEngine
    {
        public string GenerateHTML(string template, object model)
        {
            var csharpCode = this.GenerateCSharpCodeFrom(template);

            var viewObject = this.GenerateViewObjectFrom(csharpCode, model);

            return viewObject.ExecuteTemplate(model);
        }

        private string GenerateCSharpCodeFrom(string template)
        {
            var csharpCode = @"
                using MyWebServer.MVCFramework.ViewEngine;

                namespace ViewNamespace 
                {
                    public class View : IView 
                    {
                        public string ExecuteTemplate(object model)
                        {
                            var html = " + this.GenerateHtmlBody(template) + @"
                            
                            return html;
                        }
                    }
                }
            ";

            return csharpCode;
        }

        private string GenerateHtmlBody(string template)
        {
            throw new NotImplementedException();
        }

        private IView GenerateViewObjectFrom(string csharpCode, object model)
        {
            var cSharpCompilation = CSharpCompilation
                .Create("ViewAssembly")
                .WithOptions(
                    new CSharpCompilationOptions(
                        OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference
                    .CreateFromFile(
                        typeof(object).Assembly.Location))
                .AddReferences(MetadataReference
                    .CreateFromFile(
                        typeof(IView).Assembly.Location));

            if (model != null)
            {
                cSharpCompilation = cSharpCompilation
                    .AddReferences(MetadataReference
                        .CreateFromFile(model
                            .GetType().Assembly.Location));
            }

            var libraries = Assembly.Load("netstandard")
                .GetReferencedAssemblies();

            foreach (var lib in libraries)
            {
                cSharpCompilation = cSharpCompilation
                    .AddReferences(MetadataReference
                        .CreateFromFile(Assembly
                            .Load(lib).Location));
            }

            cSharpCompilation = cSharpCompilation
                .AddSyntaxTrees(SyntaxFactory
                    .ParseSyntaxTree(csharpCode));

            using var memoryStream = new MemoryStream();

            var emitResult = cSharpCompilation.Emit(memoryStream);

            if (emitResult.Success)


            return null;
        }
    }
}
