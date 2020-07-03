namespace PdfGenerator.CommandLineOptions.CommandHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PdfGenerator.CommandLineOptions.VerbInterfaces;

    public class CommandLineCommandHandlerComposition
    {
        private readonly IEnumerable<ICommandLineCommandHandler> _handlers;

        public CommandLineCommandHandlerComposition(IEnumerable<ICommandLineCommandHandler> handlers)
        {
            _handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
        }

        public void Handle(ICommandLineCommand command)
        {
            var handler = _handlers.FirstOrDefault(h => h.CanHandle(command));
            if (handler is null)
                throw new Exception("Cannot handle command");

            handler.Handle(command);
        }
    }
}