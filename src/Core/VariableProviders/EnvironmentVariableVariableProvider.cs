namespace Core.VariableProviders
{
    using System;
    using System.Collections.Generic;

    public class EnvironmentVariableVariableProvider : IVariableProvider
    {
        private const string PREFIX = "Env.";

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
            var envKey = key.Substring(prefixLength, key.Length - prefixLength);
            var result = Environment.GetEnvironmentVariable(envKey) ?? string.Empty;

            return result;
        }

        public IEnumerable<VariableDescription> Get()
        {
            yield return new VariableDescription(PREFIX + "<name>", "Replace <name> with the environment variable you request.");
        }
    }
}