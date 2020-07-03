namespace PdfGenerator.CommandLineCommandHandlers
{
    using PdfGenerator.CommandLineOptions;
    using PdfGenerator.CommandLineOptions.VerbInterfaces;

    public interface ICommandLineCommandHandler
    {
        bool CanHandle(ICommandLineCommand command);

        void Handle(ICommandLineCommand command);
    }
}