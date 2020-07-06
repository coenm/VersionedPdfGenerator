namespace Core.Tests.Integration
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
    using VariableProvider.Git;
    using VariableProvider.GitVersion;
    using Xunit;
    using Xunit.Abstractions;

    public class IntegrationTests
    {
        private readonly ITestOutputHelper _output;
        private readonly List<IVariableProvider> _providers;

        public IntegrationTests(ITestOutputHelper  output)
        {
            _output = output;
            var dateTimeFormatter = new ConfigurableDateTimeFormatter(
                                                                      "yyyy-M-d HH.mm.ss",
                                                                      "yyyy-M-d",
                                                                      "HH.mm.ss");
            var stringFormatter = new StringFormatter();

            _providers = new List<IVariableProvider>
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
                                new EnvironmentVariableVariableProvider(stringFormatter),
                                new GitVariableProviderComposition(),
                                new GitVersionVariableProviderComposition(dateTimeFormatter),
                            };
        }

        [Theory]
        [InlineData(
            "{filepath}{PathSeparator}{now}t a{time} aap {date} {env.OS} xx {empty}{fileextension} me  {filenamebase}.pdf ",
            "[D:\\aap\\beer\\cobra][\\][2020-12-1 15.22.23][t a][15.22.23][ aap ][2020-12-1][ ][Windows_NT][ xx ][][.docx][ me  ][File 234 Final][.pdf ]")]

        [InlineData(
            "{filepath}{Pathseparator}{now:yyyy}t a{time} aap {date} {env.OS} xx {empty}{fileextension} me  {filenamebase}.pdf ",
            "[D:\\aap\\beer\\cobra][\\][2020][t a][15.22.23][ aap ][2020-12-1][ ][Windows_NT][ xx ][][.docx][ me  ][File 234 Final][.pdf ]")]

        [InlineData(
            "{filepath}{pathSeparator}{now:yyyy}t a{time} aap {date} {env.OS:lower} xx {empty}{fileextension:upper} me  {filenamebase}.pdf ",
            "[D:\\aap\\beer\\cobra][\\][2020][t a][15.22.23][ aap ][2020-12-1][ ][windows_nt][ xx ][][.DOCX][ me  ][File 234 Final][.pdf ]")]

        [InlineData(
            "rubbish {gitversion.sha}{GitVersion.ShortShA}.pdf ",
            "[rubbish ][4d9353f04138567556ed8c547bc10564fa80be67][4d9353f][.pdf ]")]

        [InlineData(
            "{filenamebase}_v{GitVersion.MajorMinorPatch}.pdf ",
            "[File 234 Final][_v][0.1.0][.pdf ]")]

        [InlineData("Fixed ",                "[Fixed ]")]
        [InlineData(" Fixed",                "[ Fixed]")]
        [InlineData("Fixed",                 "[Fixed]")]
        [InlineData("F:i_x.e-d",             "[F:i_x.e-d]")]
        [InlineData("{now:yyyy:MM _.-  dd}", "[2020:12 _.-  01]")]
        [InlineData("{env.OS:lower}",        "[windows_nt]")]
        [InlineData("{env.OS:upper}",        "[WINDOWS_NT]")]
        [InlineData("{env.OS}",              "[Windows_NT]")]
        public void Parse(string input, string expectedOutput)
        {
            // arrange
            var ctx = new Context(
                                  new DateTime(2020, 12, 1, 15, 22, 23),
                                  @"D:\aap\beer\cobra\File 234 Final.docx");
            var visitor = new LanguageVisitor(_providers, ctx, true);

            // act
            var context = GetExpressionContext(input);
            var result = visitor.Visit(context);

            // assert
            Assert.Equal(expectedOutput, result);
        }

        private LanguageParser.ExpressionContext GetExpressionContext(string input)
        {
            var inputStream = new AntlrInputStream(input);
            var lexer = new LanguageLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new LanguageParser(commonTokenStream);
            var result =  parser.expression();

            _output.WriteLine($"input: '{input}'");
            _output.WriteLine(string.Empty);

            _output.WriteLine("-- TokenStream --");
            _output.WriteLine(string.Empty);

            foreach (var token in commonTokenStream.GetTokens())
            {
                var displayName = "EOF";

                if (token.Type != -1)
                    displayName = lexer.Vocabulary.GetDisplayName(token.Type);

                _output.WriteLine(token.ToString()?.Replace($"<{token.Type}>", $"<{displayName}>"));
            }

            return result;
        }
    }
}
