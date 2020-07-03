namespace PdfGenerator.WordInterop
{
    using System.Collections.Generic;

    public interface IPdfGenerator
    {
        void Generate(string wordDocumentFilename, string outputPdfFilename, Dictionary<string, string> docVars);
    }
}