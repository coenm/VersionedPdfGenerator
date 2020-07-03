namespace PdfGenerator.CommandLineOptions.VerbInterfaces
{
    using System.Collections.Generic;

    public interface IVariablesOption
    {
        IEnumerable<string> AdditionalVariables { get; }
    }
}