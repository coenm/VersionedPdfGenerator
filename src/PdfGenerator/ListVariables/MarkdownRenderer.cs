namespace PdfGenerator.ListVariables
{
    using System.Collections.Generic;
    using System.Text;

    internal class MarkdownRenderer : IDocVariableRenderer
    {
        public string Render(List<VariableInformation> information)
        {
            var sb = new StringBuilder();

            sb.AppendLine("| Variable | Description |");
            sb.AppendLine("| --- | --- |");

            foreach (var item in information)
            {
                // sb.AppendLine($"| {TransformString(item.VariableName)} | {TransformString(item.VariableDescription)} |");
                sb.Append("| ");
                sb.Append(TransformString(item.VariableName));
                sb.Append(" | ");
                sb.Append(TransformString(item.VariableDescription));
                sb.AppendLine(" |");
            }

            return sb.ToString();
        }

        private static string TransformString(string input)
        {
            if (input == null)
                return string.Empty;

            // This is a quick and dirty transformation.
            // I Know there is a '<name>' part in a string and i want to replace this to make it readable in Markdown.
            // also escape backslash.
            return input
                   .Replace("<name>", "*name*")
                   .Replace("\\", "\\\\");
        }
    }
}