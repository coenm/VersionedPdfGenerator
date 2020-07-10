namespace PdfGenerator.CommandLineOptions.CommandHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Formatters;
    using Core.VariableProviders;
    using Core.VariableProviders.DateTime;
    using Core.VariableProviders.FileInfo;
    using PdfGenerator.CommandLineOptions.Verbs;
    using PdfGenerator.Commands;
    using VariableProvider.Git;
    using VariableProvider.GitVersion;

    public class OptionsListAllVariablesCommandHandler : ICommandLineCommandHandler
    {
        public bool CanHandle(ICommandLineCommand command)
        {
            return command is ListAllVariableOptions;
        }

        public void Handle(ICommandLineCommand command)
        {
            if (!(command is ListAllVariableOptions generateConfigOptionsCommand))
                throw new ArgumentNullException(nameof(command));

            Execute(new ListVariablesCommand());
        }

        private static void Execute(ListVariablesCommand command)
        {
            var dateTimeFormatter = new ConfigurableDateTimeFormatter(string.Empty, string.Empty, string.Empty);
            var stringFormatter = new StringFormatter();

            var providers = new List<IVariableDescriptor>
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

            var maxKeyLength = providers.SelectMany(x => x.Get()).Select(x => x.Key.Length).Max();

            foreach (var provider in providers)
            {
                foreach (var description in provider.Get())
                {
                    Console.WriteLine($" - {("{" + description.Key + "}").PadRight(maxKeyLength + 2)}  :  {description.Description}");
                }
            }
        }
    }
}