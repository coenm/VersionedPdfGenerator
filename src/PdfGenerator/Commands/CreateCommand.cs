namespace PdfGenerator.Commands
{
    using System.Collections.Generic;

    internal class CreateCommand : IApplicationCommand
    {
        public Dictionary<string, string> Variables { get; set; }

        public string InputFile { get; set; }

        public string OutputFile { get; set; }

        public bool ForceOutput { get; set; }

        public string DefaultDateFormat { get; set; }

        public string DefaultTimeFormat { get; set; }

        public string DefaultDateTimeFormat { get; set; }
    }
}