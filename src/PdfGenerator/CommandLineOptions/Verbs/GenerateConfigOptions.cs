namespace PdfGenerator.CommandLineOptions.Verbs
{
    using CommandLine;

    using PdfGenerator.CommandLineOptions.VerbInterfaces;

    [Verb("generate-config") ]
    public class GenerateConfigOptions : ICommandLineCommand
    {
        [Value(0, Required = true, HelpText = "Output filename")]
        public string OutputFilename { get; set; }
    }
}