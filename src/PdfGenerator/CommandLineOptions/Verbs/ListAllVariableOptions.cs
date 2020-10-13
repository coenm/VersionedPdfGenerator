namespace PdfGenerator.CommandLineOptions.Verbs
{
    using CommandLine;

    [Verb("list-vars")]
    public class ListAllVariableOptions : ICommandLineCommand
    {
        [Option('f', "format", Required = false, Default = OutputFormat.Text, HelpText = "Output format.")]
        public OutputFormat Format { get; set; }
    }
}