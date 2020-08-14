namespace PdfGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using Core;
    using Core.Config;
    using PdfGenerator.CommandLineOptions;
    using PdfGenerator.CommandLineOptions.CommandHandlers;
    using PdfGenerator.CommandLineOptions.Verbs;
    using PdfGenerator.WordInterop;
    using VariableProvider.Git.ConfigFileLocators;

    public class Program
    {
        static void Main(string[] args)
        {
            var filenames = new[]
                                {
                                    ".VersionedPdfGenerator.yaml",
                                    ".VersionedPdfGenerator.yml",
                                    "VersionedPdfGenerator.yaml",
                                    "VersionedPdfGenerator.yml"
                                };

            var configFileLocators = new List<IDynamicConfigFileLocator>
                                         {
                                             new InputFileLocator(filenames),
                                             new GitWorkingDirFileLocator(filenames),
                                             new UserDataFileLocator(filenames),
                                         };

            var absolutePathService = new AbsolutePathService();

            var pfdGeneratorFactory = new WordInteropPdfGeneratorFactory();

            var commandLineCommandHandlers = new List<ICommandLineCommandHandler>
                                                 {
                                                     new OptionsCreateCommandHandler(absolutePathService, configFileLocators, pfdGeneratorFactory),
                                                     new OptionsGenerateConfigCommandHandler(),
                                                     new OptionsListAllVariablesCommandHandler(),
                                                 };

            var compositeCommandLineCommandHandler = new CommandLineCommandHandlerComposition(commandLineCommandHandlers);

            ICommandLineCommand command = null;
            try
            {
                command = CommandLineParser.Parse(args);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred. Press enter to exit.");
                Console.WriteLine(e);
            }

            if (command is null)
            {
                Console.ReadLine();
                return;
            }

            try
            {
                compositeCommandLineCommandHandler.Handle(command);
                Console.WriteLine("Done.Press enter to exit.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred. Press enter to exit.");
                Console.WriteLine(e);
            }
        }
    }
}
