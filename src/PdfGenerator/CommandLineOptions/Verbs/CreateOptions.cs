namespace PdfGenerator.CommandLineOptions.Verbs
{
    using System.Collections.Generic;

    using CommandLine;

    [Verb("create", isDefault: true, HelpText = "Create output file")]
    public class CreateOptions : ICommandLineCommand
    {
        [Value(0, Required = true, HelpText = "Input filename.")]
        public string Filename { get; set; }

        [Option('o', "output", Required = false, Default = null, HelpText = "Output filename. Can contain {variables}.")]
        public string OutputFilename { get; set; }

        [Option('c', "config", Required = false, Default = null, HelpText = "Specify a config file.")]
        public string ConfigFile { get; set; }

        [Option('f', "force", Required = false, Default = false, HelpText = "Overwrite output when already exists, cancel otherwise.")]
        public bool Force { get; set; }

        [Option("vars", Required = false, HelpText = "Additional DocVars. Format is Key=Value Key=Value Key=Value ")]
        public IEnumerable<string> AdditionalVariables { get; set; }
    }
}