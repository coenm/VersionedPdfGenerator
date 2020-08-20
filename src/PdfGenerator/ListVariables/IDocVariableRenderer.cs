namespace PdfGenerator.ListVariables
{
    using System.Collections.Generic;

    internal interface IDocVariableRenderer
    {
        string Render(List<VariableInformation> information);
    }
}