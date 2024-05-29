
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MyWebServer.Common.Constants;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace MyWebServer.MVCFramework.ViewEngine
{
    public class MyViewEngine : IViewEngine
    {
        public string GenerateHTML(string template, object? model, string? userId = null)
        {
            var csharpCode = GenerateCSharpCodeFrom(template, model);

            var viewObject = GenerateViewObjectFrom(csharpCode, model);

            return viewObject.ExecuteTemplate(model, userId);
        }

        private static string GenerateCSharpCodeFrom(string template, object? model)
        {
            var modelType = "object";

            if (model != null && !model
                .GetType().IsGenericType)
            {
                modelType = model
                    .GetType().FullName;
            }

            if (model != null && model
                .GetType().IsGenericType)
            {
                var modelName = model
                    .GetType().FullName;

                var genericArguments = model
                    .GetType().GenericTypeArguments;

                modelType = modelName![..modelName!
                    .IndexOf('`')] + $"<{string
                        .Join(',', genericArguments
                            .Select(x => x.FullName))}>";
            }

            var csharpCode = @"
                using System;
                using System.Linq;
                using System.Text;
                using MyWebServer.MVCFramework.ViewEngine;

                namespace ViewNamespace 
                {
                    public class View : IView 
                    {
                        public string ExecuteTemplate(object model, string user)
                        {
                            var User = user;

                            var Model = model as " + modelType + @";

                            var htmlBuilder = new StringBuilder();

                            " + GenerateHtmlBody(template) + @"
                            
                            return htmlBuilder.ToString().TrimEnd();
                        }
                    }
                }
            ";

            return csharpCode;
        }

        private static string GenerateHtmlBody(string template)
        {
            var csharpRegex = new Regex($@"[^$\s{ViewEngineConstants.DOUBLE_COMMA}&{ViewEngineConstants.SINGLE_COMMA}<!]+");

            var csharpCode = new StringBuilder();

            var stringReader = new StringReader(template);

            string line;

            while ((line = stringReader.ReadLine()!) != null)
            {
                if (line.Trim().StartsWith(ViewEngineConstants.AT_SIGN))
                {
                    csharpCode
                        .AppendLine(line
                            .Replace(ViewEngineConstants
                                .AT_SIGN, string.Empty));
                    continue;
                }
                else if (line.Trim().StartsWith(ViewEngineConstants.OPEN_CURVED_PARENTHESES) ||
                         line.Trim().StartsWith(ViewEngineConstants.CLOSE_CURVED_PARENTHESES))
                {
                    csharpCode.AppendLine(line);
                    continue;
                }


                csharpCode
                    .Append($"{ViewEngineConstants.APPEND_LINE}(@{ViewEngineConstants.DOUBLE_COMMA}");

                while (line.Contains(ViewEngineConstants.AT_SIGN))
                {
                    var atSignLocation = line
                        .IndexOf(ViewEngineConstants.AT_SIGN);

                    var beforeAtSing = line[..atSignLocation];

                    csharpCode
                        .Append(beforeAtSing
                            .Replace(
                                ViewEngineConstants.DOUBLE_COMMA,
                                ViewEngineConstants.TWO_DOUBLE_COMMA) + $"{ViewEngineConstants
                                .DOUBLE_COMMA} + ");

                    var afterAtSing = line[(atSignLocation + 1)..];

                    var code = csharpRegex
                        .Match(afterAtSing).Value;
                    csharpCode.Append(code + $" + @{ViewEngineConstants.DOUBLE_COMMA}");
                    line = afterAtSing[code.Length..];
                }

                csharpCode
                    .AppendLine($"{line
                        .Replace(
                            ViewEngineConstants.DOUBLE_COMMA,
                            ViewEngineConstants.TWO_DOUBLE_COMMA)}{ViewEngineConstants.DOUBLE_COMMA});");
            }


            return csharpCode.ToString().TrimEnd();
        }

        private static IView GenerateViewObjectFrom(string csharpCode, object? model)
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
                var modelType = model.GetType();

                if (modelType.IsGenericType)
                {
                    var genericArguments = modelType.GetGenericArguments();

                    foreach (var genericArgument in genericArguments)
                    {
                        cSharpCompilation = cSharpCompilation
                            .AddReferences(MetadataReference.CreateFromFile(genericArgument.FullName));
                    }
                }

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

            if (!emitResult.Success)
            {
                return new ErrorView(emitResult.Diagnostics
                    .Where(x => x.Severity == DiagnosticSeverity.Error)
                    .Select(x => x.GetMessage()), csharpCode);
            }

            try
            {
                return CreateViewInstance(memoryStream);
            }
            catch (Exception ex)
            {

                return new ErrorView([ex.ToString()], csharpCode);
            }
        }

        private static IView CreateViewInstance(MemoryStream memoryStream)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
            var byteAssembly = memoryStream.ToArray();
            var assambly = Assembly.Load(byteAssembly);
            var viewType = assambly.GetType("ViewNamespace.View");
            var viewInstance = Activator.CreateInstance(viewType!);

            return (viewInstance as IView)!;
        }
    }
}
