namespace PdfGenerator.WordInterop
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading;

    using Core;
    using Microsoft.Office.Interop.Word;

    // https://dottutorials.net/programmatically-generate-word-files-using-interop-in-net-core/

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
            var filename = (object)wordDocumentFilename;
            var readonlyMode = (object)true;

            // Use the dummy value as a placeholder for optional arguments
            Document doc = wordApplication.Documents.Open(
                                                          ref filename,
                                                          ref oMissing, ref readonlyMode, ref oMissing,
                                                          ref oMissing, ref oMissing, ref oMissing,
                                                          ref oMissing, ref oMissing, ref oMissing,
                                                          ref oMissing, ref oMissing, ref oMissing,
                                                          ref oMissing, ref oMissing, ref oMissing);

            // temp stupid workaround.
            Thread.Sleep(5000);

            doc.Activate();

            foreach (var item in docVars)
            {
                doc.Variables.Add(item.Key, string.IsNullOrEmpty(item.Value) ? " " : item.Value);
            }

            UpdateFields(doc);

            try
            {
                doc.ExportAsFixedFormat(
                                        outputPdfFilename,
                                        WdExportFormat.wdExportFormatPDF,
                                        false,
                                        WdExportOptimizeFor.wdExportOptimizeForPrint,
                                        WdExportRange.wdExportAllDocument,
                                        1,
                                        1,
                                        WdExportItem.wdExportDocumentContent,
                                        true,
                                        true,
                                        WdExportCreateBookmarks.wdExportCreateWordBookmarks);
            }
            catch
            {
                object outputFileName = outputPdfFilename;
                object fileFormat = WdSaveFormat.wdFormatPDF;

                // Save document into PDF Format
                doc.SaveAs(
                           ref outputFileName, ref fileFormat,
                           ref oMissing, ref oMissing, ref oMissing,
                           ref oMissing, ref oMissing, ref oMissing,
                           ref oMissing, ref oMissing, ref oMissing,
                           ref oMissing, ref oMissing, ref oMissing,
                           ref oMissing, ref oMissing);
            }

            // Close the Word document, but leave the Word application open.
            // doc has to be cast to type _Document so that it will find the
            // correct Close method.
            object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
            ((_Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);

            // word has to be cast to type _Application so that it will find
            // the correct Quit method.
            ((_Application)wordApplication).Quit(ref oMissing, ref oMissing, ref oMissing);

            Marshal.ReleaseComObject(doc);
        }

        private static void UpdateFields(Document doc)
        {
            doc.Fields.Update();

            foreach (var field in GetAllFields(doc))
            {
                field.Update();
            }
        }

        // taken from https://stackoverflow.com/questions/31964938/how-to-update-fields-in-headers-and-footers-not-just-main-document
        private static IEnumerable<Field> GetAllFields(Document document)
        {
            // Main text story fields (doesn't include fields in headers and footers)
            foreach (Field field in document.Fields)
            {
                yield return field;
            }

            foreach (Section section in document.Sections)
            {
                // Header fields
                foreach (HeaderFooter header in section.Headers)
                {
                    foreach (Field field in header.Range.Fields)
                    {
                        yield return field;
                    }
                }

                // Footer fields
                foreach (HeaderFooter footer in section.Footers)
                {
                    foreach (Field field in footer.Range.Fields)
                    {
                        yield return field;
                    }
                }
            }
        }
    }
}
