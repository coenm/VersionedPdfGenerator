namespace PdfGenerator.CommandLineOptions.CommandHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.VariableProviders;
    using PdfGenerator.CommandLineOptions.Verbs;
    using PdfGenerator.Commands;

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

            Execute(new ListVariablesCommand());
        }

        private void Execute(ListVariablesCommand command)
        {
            var maxKeyLength = _variableProvider.SelectMany(x => x.Get()).Select(x => x.Key.Length).Max();

            foreach (var provider in _variableProvider)
            {
                foreach (var description in provider.Get())
                {
                    Console.WriteLine($" - {("{" + description.Key + "}").PadRight(maxKeyLength + 2)}  :  {description.Description}");
                }
            }
        }
    }
}