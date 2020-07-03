namespace PdfGenerator.CommandLineCommandHandlers
{
    using PdfGenerator.CommandLineOptions;
    using PdfGenerator.CommandLineOptions.VerbInterfaces;
    using PdfGenerator.CommandLineOptions.Verbs;

    public class CreateOptionsCommandHandler : ICommandLineCommandHandler
    {
        public bool CanHandle(ICommandLineCommand command)
        {
            return command is CreateOptions;
        }

        public void Handle(ICommandLineCommand command)
        {
            var c = command as CreateOptions;

        }
    }
}