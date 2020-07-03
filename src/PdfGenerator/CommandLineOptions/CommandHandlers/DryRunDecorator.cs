namespace PdfGenerator.CommandLineOptions.CommandHandlers
{
    using System.Collections.Generic;

    using PdfGenerator.WordInterop;

    internal class DryRunDecorator : IPdfGenerator
    {
        public DryRunDecorator(IPdfGenerator decoratee)
        {
            // ignore decoratee.
        }

        public void Generate(string wordDocumentFilename, string outputPdfFilename, Dictionary<string, string> docVars)
        {
            // do nothing.
        }
    }
}