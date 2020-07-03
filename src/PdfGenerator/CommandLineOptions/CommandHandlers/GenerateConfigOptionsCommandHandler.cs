namespace PdfGenerator.CommandLineCommandHandlers
{
    using PdfGenerator.CommandLineOptions;
    using PdfGenerator.CommandLineOptions.VerbInterfaces;
    using PdfGenerator.CommandLineOptions.Verbs;

    public class GenerateConfigOptionsCommandHandler : ICommandLineCommandHandler
    {
        public bool CanHandle(ICommandLineCommand command)
        {
            return command is GenerateConfigOptions;
        }

        public void Handle(ICommandLineCommand command)
        {
            var c = command as GenerateConfigOptions;
        }
    }
}