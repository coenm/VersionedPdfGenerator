namespace PdfGenerator.CommandLineOptions.CommandHandlers
{
    using PdfGenerator.CommandLineOptions.Verbs;

    public interface ICommandLineCommandHandler
    {
        bool CanHandle(ICommandLineCommand command);

        void Handle(ICommandLineCommand command);
    }
}