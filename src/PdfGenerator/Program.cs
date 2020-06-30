namespace PdfGenerator
{
    using System;
    using System.Collections.Generic;

    using Antlr4.Runtime;
    using Core;
    using Core.Formatters;
    using Core.Parser;
    using Core.VariableProviders;
    using Core.VariableProviders.DateTime;
    using Core.VariableProviders.FileInfo;
    using PdfGenerator.WordInterop;

    class Program
    {
        static void Main(string[] args)
        {
            //var inputFilename = @"D:\aap\beer\cobra\File 234 Final.docx";
            var inputFilename = "C:\\Users\\coenm\\Documents\\coenm.docx";
            var now = DateTime.Now;
            var outputFilenameTemplate = "{filepath}{PathSeparator}{now:yyyyMMddHHmmss}_{env.username:lower}_{filenamebase}.pdf";

            var dateTimeFormatter = new ConfigurableDateTimeFormatter(
                                                                      "yyyy-M-d HH.mm.ss",
                                                                      "yyyy-M-d",
                                                                      "HH.mm.ss");
            var stringFormatter = new StringFormatter();

            var providers = new List<IVariableProvider>
                             {
                                 new DateTimeNowVariableProvider(dateTimeFormatter),
                                 new DateTimeTimeVariableProvider(dateTimeFormatter),
                                 new DateTimeDateVariableProvider(dateTimeFormatter),
                                 new FilenameBaseVariableProvider(),
                                 new FilenameVariableProvider(),
                                 new FilePathVariableProvider(),
                                 new FileExtensionVariableProvider(stringFormatter),
                                 new PathSeparatorVariableProvider(),
                                 new EmptyVariableProvider(),
                                 new EnvironmentVariableVariableProvider(stringFormatter)
                                 /*new GitVersionVariableProvider(new GitVersionJsonReader(JSON_CONTENT))*/,
                             };

            var ctx = new Context(now, inputFilename);

            var visitor = new LanguageVisitor(providers, ctx);
            var outputFilename = visitor.Visit(GetExpressionContext(outputFilenameTemplate));

            var generator = new GeneratePdf();
            var docVars = new Dictionary<string, string>
                              {
                                  { "GitVersion", "[v.1.0.0]" }
                              };
            generator.Generate(inputFilename, outputFilename, docVars);
        }

        private static LanguageParser.ExpressionContext GetExpressionContext(string input)
        {
            var inputStream = new AntlrInputStream(input);
            var lexer = new LanguageLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new LanguageParser(commonTokenStream);
            return parser.expression();
        }
    }
}
