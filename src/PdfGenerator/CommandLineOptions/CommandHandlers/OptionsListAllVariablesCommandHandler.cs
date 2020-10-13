namespace PdfGenerator.CommandLineOptions.CommandHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.VariableProviders;
    using PdfGenerator.CommandLineOptions.Verbs;
    using PdfGenerator.Commands;
    using PdfGenerator.ListVariables;

    public class OptionsListAllVariablesCommandHandler : ICommandLineCommandHandler
    {
        private readonly List<IVariableProvider> _variableProvider;

        public OptionsListAllVariablesCommandHandler(List<IVariableProvider> variableProviders)
        {
            _variableProvider = variableProviders ?? new List<IVariableProvider>(0);
        }

        public bool CanHandle(ICommandLineCommand command)
        {
            return command is ListAllVariableOptions;
        }

        public void Handle(ICommandLineCommand command)
        {
            if (!(command is ListAllVariableOptions generateConfigOptionsCommand))
                throw new ArgumentNullException(nameof(command));

            var listVariablesCommand = new ListVariablesCommand
                                           {
                                               UseMarkdown = generateConfigOptionsCommand.Format == OutputFormat.Markdown,
                                           };

            Execute(listVariablesCommand);
        }

        private void Execute(ListVariablesCommand command)
        {
            var variableInformation = new List<VariableInformation>();

            foreach (var provider in _variableProvider)
            {
                variableInformation.AddRange(provider.Get().Select(description => new VariableInformation(provider.GetType().Name, description.Key, description.Description)));
            }

            IDocVariableRenderer renderer;
            if (command.UseMarkdown)
                renderer = new MarkdownRenderer();
            else
                renderer = new ConsoleRenderer();

            var content = renderer.Render(variableInformation);
            Console.WriteLine(content);
        }
    }
}