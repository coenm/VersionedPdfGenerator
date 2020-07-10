namespace PdfGenerator.CommandLineOptions.CommandHandlers
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Antlr4.Runtime;
    using Core;
    using Core.Config;
    using Core.Formatters;
    using Core.Parser;
    using Core.VariableProviders;
    using Core.VariableProviders.DateTime;
    using Core.VariableProviders.FileInfo;
    using PdfGenerator.CommandLineOptions.Verbs;
    using PdfGenerator.Commands;
    using PdfGenerator.ConfigFile;
    using PdfGenerator.WordInterop;
    using VariableProvider.Git;
    using VariableProvider.GitVersion;
    using YamlDotNet.Serialization;

    public class OptionsCreateCommandHandler : ICommandLineCommandHandler
    {
        private readonly IAbsolutePathService _absolutePathService;
        private readonly List<IDynamicConfigFileLocator> _configFileLocator;

        public OptionsCreateCommandHandler(IAbsolutePathService absolutePathService, List<IDynamicConfigFileLocator> configFileLocator)
        {
            _absolutePathService = absolutePathService ?? throw new ArgumentNullException(nameof(absolutePathService));
            _configFileLocator = configFileLocator ?? throw new ArgumentNullException(nameof(configFileLocator));
        }

        public bool CanHandle(ICommandLineCommand command)
        {
            return command is CreateOptions;
        }

        public void Handle(ICommandLineCommand command)
        {
            if (!(command is CreateOptions createOptions))
                throw new ArgumentNullException(nameof(command));

            var inputFilename = _absolutePathService.GetExistingAbsoluteFilename(createOptions.Filename);
            if (inputFilename == null)
                throw new Exception("Could not find input file");

            Config config = null;

            if (!string.IsNullOrWhiteSpace(createOptions.ConfigFile))
            {
                var filename = createOptions.ConfigFile;
                config = LoadConfig(filename);
            }

            if (config == null)
            {
                // find other config file and load.
                foreach (var configFileLocator in _configFileLocator)
                {
                    foreach (var f in configFileLocator.Locate(inputFilename))
                    {
                        config ??= LoadConfig(f);
                    }
                }
            }

            var variables = new Dictionary<string, string>();
            string outputFilename = null;
            var forceOutput = false;
            var defaultTimeFormat = "HH.mm.ss";
            var defaultDateFormat = "yyyy-M-d";
            var defaultDateTimeFormat = "yyyy-M-d HH.mm.ss";

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

                if (string.IsNullOrWhiteSpace(config.OutputPath))
                    outputFilename = Path.Combine(config.OutputFilename);
                else if (string.IsNullOrWhiteSpace(config.OutputFilename))
                    outputFilename = Path.Combine(config.OutputPath, new FileInfo(inputFilename).Name + ".pdf");
                else
                    outputFilename = Path.Combine(config.OutputPath, config.OutputFilename);

                if (config.OverwriteOutputWhenExist.HasValue)
                    forceOutput = config.OverwriteOutputWhenExist.Value;

                if (config.DefaultFormats != null)
                {
                    if (!string.IsNullOrWhiteSpace(config.DefaultFormats.DateFormat))
                        defaultDateFormat = config.DefaultFormats.DateFormat;

                    if (!string.IsNullOrWhiteSpace(config.DefaultFormats.DateTimeFormat))
                        defaultDateTimeFormat = config.DefaultFormats.DateTimeFormat;

                    if (!string.IsNullOrWhiteSpace(config.DefaultFormats.TimeFormat))
                        defaultTimeFormat = config.DefaultFormats.TimeFormat;
                }
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
            {
                // todo make absolute path.
                outputFilename = createOptions.OutputFilename;
            }

            if (createOptions.Force)
                forceOutput = createOptions.Force;

            var createCommand =  new CreateCommand
                {
                    InputFile = inputFilename,
                    OutputFile = outputFilename,
                    Variables = variables,
                    ForceOutput = forceOutput,
                    DefaultTimeFormat = defaultTimeFormat,
                    DefaultDateFormat = defaultDateFormat,
                    DefaultDateTimeFormat = defaultDateTimeFormat,
                };

            Execute(createCommand);
        }

        private static Config LoadConfig(string filename)
        {
            var fullConfigFilename = string.Empty;

            if (File.Exists(filename))
            {
                var result = new FileInfo(filename);
                fullConfigFilename = result.FullName;
            }

            if (string.IsNullOrWhiteSpace(fullConfigFilename))
                return null;

            try
            {
                using var fileStream = new FileStream(fullConfigFilename, FileMode.Open);
                using var reader = new StreamReader(fileStream);
                var deserializer = new Deserializer();
                return deserializer.Deserialize<Config>(reader);
            }
            catch (Exception)
            {
                // do nothing.
            }

            return null;
        }

        private static void Execute(CreateCommand command)
        {
            var dateTimeFormatter = new ConfigurableDateTimeFormatter(
                                                                      command.DefaultDateTimeFormat,
                                                                      command.DefaultDateFormat,
                                                                      command.DefaultTimeFormat);
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
                                    new GitVariableProviderComposition(dateTimeFormatter),
                                    new GitVersionVariableProviderComposition(dateTimeFormatter),
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

            IPdfGenerator generator = new WordInteropPdfGenerator(showAnimation:false, wordVisible:false, screenUpdating:false);

            Console.WriteLine("OUTPUT filename: "+ outputFilename);

            if (!command.ForceOutput && File.Exists(outputFilename))
                return;

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