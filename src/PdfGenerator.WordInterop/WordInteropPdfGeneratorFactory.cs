namespace PdfGenerator.WordInterop
{
    using Core;

    public class WordInteropPdfGeneratorFactory : IPdfGeneratorFactory
    {
        public IPdfGenerator Create()
        {
            return new WordInteropPdfGenerator(true, true, true);
        }
    }
}