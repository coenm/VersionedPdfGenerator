namespace PdfGenerator.Commands
{
    internal class CreateConfigCommand : IApplicationCommand
    {
        public string OutputFilename { get; set; }
    }
}