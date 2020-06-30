namespace PdfGenerator.WordInterop
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Microsoft.Office.Interop.Word;

    public class GeneratePdf
    {
        public void Generate(string wordDocumentFilename, string outputPdfFilename, Dictionary<string, string> docVars)
        {
            var winword = new Application
                              {
                                  ShowAnimation = false,
                                  Visible = true,
                                  ScreenUpdating = false
                              };

            object oMissing = Missing.Value;

            {
                // Cast as Object for word Open method
                Object filename = (Object)wordDocumentFilename;

                // Use the dummy value as a placeholder for optional arguments
                Document doc = winword.Documents.Open(ref filename, ref oMissing,
                                                   ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                                                   ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                                                   ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                doc.Activate();

                // var properties = doc.CustomDocumentProperties;
                // pro
                //
                // if (properties.Cast<DocumentProperty>().Where(c => c.Name == "DocumentID").Count() == 0)
                //     properties.Add("DocumentID", false, MsoDocProperties.msoPropertyTypeString, Guid.NewGuid().ToString());
                // var docID = properties["DocumentID"].Value.ToString();

                // Object name = (object)"CoenGitVersion";
                // var variable = doc.Variables.get_Item(name);
                // var value = variable.Value;

                // doc.Variables.Add("GitVersion", (object)"abc-def.1.0.0.0");

                foreach (var item in docVars)
                {
                    doc.Variables.Add(item.Key, (object)item.Value);
                }

                object outputFileName = outputPdfFilename;
                object fileFormat = WdSaveFormat.wdFormatPDF;

                // Save document into PDF Format
                doc.SaveAs(ref outputFileName,
                           ref fileFormat, ref oMissing, ref oMissing,
                           ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                           ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                           ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                // Close the Word document, but leave the Word application open.
                // doc has to be cast to type _Document so that it will find the
                // correct Close method.
                object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
                ((_Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);
                doc = null;
            }

            // word has to be cast to type _Application so that it will find
            // the correct Quit method.
            ((_Application)winword).Quit(ref oMissing, ref oMissing, ref oMissing);
            winword = null;

            //
            // //Set status for word application is to be visible or not.
            // winword.Visible = true;
            //
            //
            // Document document = winword.Documents.Open();
            //
            // var vars = document.Variables;
            //
            // document.Close();
        }
    }
}
