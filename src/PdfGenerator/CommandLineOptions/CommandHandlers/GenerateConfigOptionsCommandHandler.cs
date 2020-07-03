namespace PdfGenerator.CommandLineOptions.CommandHandlers
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using PdfGenerator.CommandLineOptions.Verbs;
    using PdfGenerator.Commands;
    using PdfGenerator.ConfigFile;

    public class GenerateConfigOptionsCommandHandler : ICommandLineCommandHandler
    {
        public bool CanHandle(ICommandLineCommand command)
        {
            return command is GenerateConfigOptions;
        }

        public void Handle(ICommandLineCommand command)
        {
            if (!(command is GenerateConfigOptions generateConfigOptionsCommand))
                throw new ArgumentNullException(nameof(command));

            Execute(new CreateConfigCommand
                        {
                            OutputFilename = generateConfigOptionsCommand.OutputFilename,
                        });
        }

        private static void Execute(CreateConfigCommand command)
        {
            var configFile = new Config
                                 {
                                     DefaultFormats = new DefaultFormats
                                                          {
                                                              DateFormat = "yyyy-MM-dd",
                                                              DateTimeFormat = "yyyy-MM-dd HH.mm.ss",
                                                              TimeFormat = "HH.mm.ss",
                                                          },
                                     OutputPath = "{filepath}",
                                     OutputFilename = "{filenamebase}.pdf",
                                     GitVersionJsonFile = "{filepath}{PathSeparator}GitVersion.json",
                                     OverwriteOutputWhenExist = true,
                                     DocVariables = new Dictionary<string, string>
                                                        {
                                                            { "VERSION", "v.{gitversion.sha}   " },
                                                            { "RENDER_DATETIME", "{now:yyyy-MM-dd HH:mm:ss}" },
                                                        },
                                 };

            var serializer = new YamlDotNet.Serialization.Serializer();

            using var fileStream = new FileStream(command.OutputFilename, FileMode.Create);
            using var writer = new StreamWriter(fileStream);
            serializer.Serialize(writer, configFile);
        }
    }
}