namespace PdfGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Core;
    using Core.Config;
    using Core.VariableProviders;
    using PdfGenerator.CommandLineOptions;
    using PdfGenerator.CommandLineOptions.CommandHandlers;
    using PdfGenerator.CommandLineOptions.Verbs;
    using PdfGenerator.WordInterop;
    using VariableProvider.Git.ConfigFileLocators;
    using WebHost;

    public class Program
    {
        [STAThread]
        public static async Task Main(string[] args)
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

            var enabledModules = new List<IModule>
                                     {
                                         new WebHostModule(),
                                     };
            try
            {
                foreach (var module in enabledModules)
                    await module.InitializeAsync();

                foreach (var module in enabledModules)
                    await module.StartAsync();

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

                var moduleVariableProviders = enabledModules.SelectMany(x => x.CreateVariableProviders()).ToList();
                var moduleVariableDescriptors = moduleVariableProviders
                                                .Select(x => x as IVariableDescriptor)
                                                .Where(x => x != null)
                                                .ToList();

                var commandLineCommandHandlers = new List<ICommandLineCommandHandler>
                                                     {
                                                         new OptionsCreateCommandHandler(absolutePathService, configFileLocators, pfdGeneratorFactory, moduleVariableProviders),
                                                         new OptionsGenerateConfigCommandHandler(),
                                                         new OptionsListAllVariablesCommandHandler(moduleVariableDescriptors),
                                                     };

                var compositeCommandLineCommandHandler = new CommandLineCommandHandlerComposition(commandLineCommandHandlers);


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
            finally
            {
                foreach (var module in enabledModules)
                    await module.StopAsync();
            }

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}
