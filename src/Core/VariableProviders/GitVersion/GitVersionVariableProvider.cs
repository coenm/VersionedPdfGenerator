namespace Core.VariableProviders.GitVersion
{
    using System;

    public class GitVersionVariableProvider : IVariableProvider
    {
        private readonly GitVersionJsonReader _reader;
        private const string PREFIX = "gitversion.";

        public GitVersionVariableProvider(GitVersionJsonReader reader)
        {
            _reader = reader;
        }

        public bool CanProvide(string key)
        {
            if (key is null)
                return false;
            if (!key.StartsWith(PREFIX, StringComparison.CurrentCultureIgnoreCase))
                return false;

            var prefixLength = PREFIX.Length;
            if (key.Length <= prefixLength)
                return false;

            var envKey = key.Substring(prefixLength, key.Length - prefixLength);

            return !string.IsNullOrWhiteSpace(envKey);
        }

        public string Provide(Context context, string key, string arg)
        {
            var prefixLength = PREFIX.Length;
            var gitVersionKey = key.Substring(prefixLength, key.Length - prefixLength);
            return _reader.GetValue(gitVersionKey);
        }
    }
}