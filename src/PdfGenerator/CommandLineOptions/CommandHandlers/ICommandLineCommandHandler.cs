namespace PdfGenerator.CommandLineOptions.CommandHandlers
{
    using PdfGenerator.CommandLineOptions.VerbInterfaces;

    public interface ICommandLineCommandHandler
    {
        bool CanHandle(ICommandLineCommand command);

        void Handle(ICommandLineCommand command);
    }
}