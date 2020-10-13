namespace PdfGenerator.CommandLineOptions.Verbs
{
    using CommandLine;

    [Verb("generate-config")]
    public class GenerateConfigOptions : ICommandLineCommand
    {
        [Value(0, Required = true, HelpText = "Output filename")]
        public string OutputFilename { get; set; }
    }
}