namespace PdfGenerator.CommandLineOptions.CommandHandlers
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Antlr4.Runtime;
    using Core;
    using Core.Formatters;
    using Core.Parser;
    using Core.VariableProviders;
    using Core.VariableProviders.DateTime;
    using Core.VariableProviders.FileInfo;
    using Core.VariableProviders.GitVersion;
    using PdfGenerator.CommandLineOptions.VerbInterfaces;
    using PdfGenerator.CommandLineOptions.Verbs;
    using PdfGenerator.Commands;
    using PdfGenerator.ConfigFile;
    using PdfGenerator.WordInterop;
    using YamlDotNet.Serialization;

    public class CreateOptionsCommandHandler : ICommandLineCommandHandler
    {
        public bool CanHandle(ICommandLineCommand command)
        {
            return command is CreateOptions;
        }

        public void Handle(ICommandLineCommand command)
        {
            if (!(command is CreateOptions createOptions))
                throw new ArgumentNullException(nameof(command));

            Config config = null;
            var fullConfigFilename = string.Empty;

            if (!string.IsNullOrWhiteSpace(createOptions.ConfigFile))
            {
                var filename = createOptions.ConfigFile;

                if (File.Exists(filename))
                {
                    var result = new FileInfo(filename);
                    fullConfigFilename = result.FullName;
                }

                if (!string.IsNullOrWhiteSpace(fullConfigFilename))
                {
                    try
                    {
                        using var fileStream = new FileStream(fullConfigFilename, FileMode.Open);
                        using var reader = new StreamReader(fileStream);
                        var deserializer = new Deserializer();
                        config = deserializer.Deserialize<Config>(reader);
                    }
                    catch (Exception)
                    {
                        // do nothing.
                    }
                }
            }

            var variables = new Dictionary<string, string>();
            string outputFilename = null;
            var forceOutput = false;

            if (config != null)
            {
                foreach (var item in config.DocVariables)
                {
                    var key = item.Key;
                    var value = item.Value;

                    if (variables.ContainsKey(key))
                        variables[key] = value;
                    else
                        variables.Add(key, value);
                }

                outputFilename = config.OutputFilename;

                if (config.OverwriteOutputWhenExist.HasValue)
                    forceOutput = config.OverwriteOutputWhenExist.Value;
            }

            char[] sep = { '=' };

            foreach (var item in createOptions.AdditionalVariables)
            {
                var split = item.Split(sep, 2, StringSplitOptions.None);
                if (split.Length != 2)
                    continue;

                var key = split[0];
                var value = split[1];

                if (variables.ContainsKey(key))
                    variables[key] = value;
                else
                    variables.Add(key, value);
            }

            if (!string.IsNullOrWhiteSpace(createOptions.OutputFilename))
                outputFilename = createOptions.OutputFilename;

            if (createOptions.Force)
                forceOutput = createOptions.Force;

            var createCommand =  new CreateCommand
                {
                    InputFile = createOptions.Filename,
                    OutputFile = outputFilename,
                    Variables = variables,
                    ForceOutput = forceOutput,
                };

            Execute(createCommand);
        }

        private static void Execute(CreateCommand command)
        {
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
                                    new EnvironmentVariableVariableProvider(stringFormatter),
                                    new GitVersionVariableProvider(new GitVersionJsonReader(GitVersionFakeContent.GITVERSION_JSON)),
                                };

            var ctx = new Context(DateTime.Now, command.InputFile);

            var visitor = new LanguageVisitor(providers, ctx);
            var outputFilename = visitor.Visit(GetExpressionContext(command.OutputFile));

            var docVars = new Dictionary<string, string>();

            foreach (var item in command.Variables)
            {
                try
                {
                    var key = item.Key;
                    var expressionContext = GetExpressionContext(item.Value);
                    var value = visitor.Visit(expressionContext);
                    docVars.Add(key, value);
                }
                catch (Exception)
                {
                    // skip
                }
            }

            var generator = new GeneratePdf();
            generator.Generate(command.InputFile, outputFilename, docVars);
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