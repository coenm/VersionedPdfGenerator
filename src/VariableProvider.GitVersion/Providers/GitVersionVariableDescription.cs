namespace VariableProvider.GitVersion.Providers
{
    internal readonly struct GitVersionVariableDescription
    {
        public GitVersionVariableDescription(string key, string description)
        {
            Key = key;
            Description = description;
        }

        public string Key { get; }

        public string Description { get;  }
    }
}