namespace PdfGenerator.CommandLineOptions.Verbs
{
    using System.Collections.Generic;

    using CommandLine;

    using PdfGenerator.CommandLineOptions.VerbInterfaces;

    [Verb("create", isDefault: true, HelpText = "Create output file")]
    public class CreateOptions : IConfigFileOption, IDryRunOption, IVariablesOption, IOutputFilenameOption, IForceOption, ICommandLineCommand
    {
        [Value(0, Required = true, HelpText = "Input filename.")]
        public string Filename { get; set; }

        [Option('o', "output", Required = false, Default = null, HelpText = "Output filename. Can have {variables} in pla")]
        public string OutputFilename { get; set; }

        [Option('c', "config", Required = false, Default = null, HelpText = "Specify a config file.")]
        public string ConfigFile { get; set; }

        [Option('f', "force", Required =  false, Default = false)]
        public bool Force { get; set; }

        [Option("dry-run", Required = false, Default = null, HelpText = "Dry Run")]
        public bool DryRun { get; set; }

        [Option("vars", Required = false, HelpText = "Additional DocVars. Format is Key=Value Key=Value Key=Value ")]
        public IEnumerable<string> AdditionalVariables { get; set; }
    }
}