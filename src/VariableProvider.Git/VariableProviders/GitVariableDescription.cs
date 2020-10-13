namespace VariableProvider.Git.VariableProviders
{
    internal readonly struct GitVariableDescription
    {
        public GitVariableDescription(string key, string description)
        {
            Key = key;
            Description = description;
        }

        public string Key { get; }

        public string Description { get;  }
    }
}