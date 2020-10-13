namespace PdfGenerator.ConfigFile
{
    using System.Collections.Generic;

    public class Config
    {
        public string OutputPath { get; set; }

        public string OutputFilename { get; set; }

        public bool? OverwriteOutputWhenExist { get; set; }

        public DefaultFormats DefaultFormats { get; set; }

        public Dictionary<string, string> DocVariables { get; set; }
    }
}