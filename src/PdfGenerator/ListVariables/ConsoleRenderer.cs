namespace PdfGenerator.ListVariables
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class ConsoleRenderer : IDocVariableRenderer
    {
        public string Render(List<VariableInformation> information)
        {
            var maxKeyLength = information.Select(x => x.VariableName.Length).Max() + 2;

            var sb = new StringBuilder();
            foreach (var item in information)
            {
                sb.AppendLine($" - {("{" + item.VariableName + "}").PadRight(maxKeyLength)}  :  {item.VariableDescription}");
            }

            return sb.ToString();
        }
    }
}