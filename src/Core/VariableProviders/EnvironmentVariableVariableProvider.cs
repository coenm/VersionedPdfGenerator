namespace Core.VariableProviders
{
    using System;

    using Core.Formatters;

    public class EnvironmentVariableVariableProvider : IVariableProvider
    {
        private readonly IStringFormatter _stringFormatter;
        private const string PREFIX = "env.";

        public EnvironmentVariableVariableProvider(IStringFormatter stringFormatter)
        {
            _stringFormatter = stringFormatter ?? throw new ArgumentNullException(nameof(stringFormatter));
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
            var envKey = key.Substring(prefixLength, key.Length - prefixLength);
            var result = Environment.GetEnvironmentVariable(envKey) ?? string.Empty;

            return string.IsNullOrWhiteSpace(arg)
                       ? result
                       : _stringFormatter.Format(result, arg);
        }
    }
}