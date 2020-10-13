namespace PdfGenerator.ListVariables
{
    internal readonly struct VariableInformation
    {
        public VariableInformation(string providerName, string variableName, string variableDescription)
        {
            ProviderName = providerName;
            VariableName = variableName;
            VariableDescription = variableDescription;
        }

        public string ProviderName { get; }

        public string VariableName { get; }

        public string VariableDescription { get;  }
    }
}