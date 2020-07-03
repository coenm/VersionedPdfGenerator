namespace PdfGenerator.WordInterop
{
    using System.Collections.Generic;
    using System.Reflection;

    using Microsoft.Office.Interop.Word;

    public class WordInteropPdfGenerator : IPdfGenerator
    {
        private readonly bool _showAnimation;
        private readonly bool _wordVisible;
        private readonly bool _screenUpdating;

        public WordInteropPdfGenerator(bool showAnimation, bool wordVisible, bool screenUpdating)
        {
            _showAnimation = showAnimation;
            _wordVisible = wordVisible;
            _screenUpdating = screenUpdating;
        }

        public void Generate(string wordDocumentFilename, string outputPdfFilename, Dictionary<string, string> docVars)
        {
            var wordApplication = new Application
                                      {
                                          ShowAnimation = _showAnimation,
                                          Visible = _wordVisible,
                                          ScreenUpdating = _screenUpdating,
                                      };

            object oMissing = Missing.Value;

            // Cast as Object for word Open method
            object filename = (object)wordDocumentFilename;

            // Use the dummy value as a placeholder for optional arguments
            Document doc = wordApplication.Documents.Open(ref filename,
                                                          ref oMissing, ref oMissing, ref oMissing,
                                                          ref oMissing, ref oMissing, ref oMissing,
                                                          ref oMissing, ref oMissing, ref oMissing,
                                                          ref oMissing, ref oMissing, ref oMissing,
                                                          ref oMissing, ref oMissing, ref oMissing);
            doc.Activate();

            foreach (var item in docVars)
            {
                doc.Variables.Add(item.Key, (object)item.Value);
            }

            object outputFileName = outputPdfFilename;

            object fileFormat = WdSaveFormat.wdFormatPDF;

            // Save document into PDF Format
            doc.SaveAs(ref outputFileName, ref fileFormat,
                       ref oMissing, ref oMissing, ref oMissing,
                       ref oMissing, ref oMissing, ref oMissing,
                       ref oMissing, ref oMissing, ref oMissing,
                       ref oMissing, ref oMissing, ref oMissing,
                       ref oMissing, ref oMissing);

            // Close the Word document, but leave the Word application open.
            // doc has to be cast to type _Document so that it will find the
            // correct Close method.
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            ((_Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);
            doc = null;

            // word has to be cast to type _Application so that it will find
            // the correct Quit method.
            ((_Application)wordApplication).Quit(ref oMissing, ref oMissing, ref oMissing);
            wordApplication = null;
        }
    }
}
