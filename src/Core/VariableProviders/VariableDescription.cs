namespace Core.VariableProviders
{
    public readonly struct VariableDescription
    {
        public VariableDescription(string key, string description)
        {
            Key = key;
            Description = description;
        }

        public string Key { get; }

        public string Description { get; }
    }
}