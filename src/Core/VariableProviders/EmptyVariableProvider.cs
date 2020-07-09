namespace Core.VariableProviders
{
    using System;

    public class EmptyVariableProvider : IVariableProvider
    {
        private const string KEY = "empty";

        public bool CanProvide(string key) => KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);

        public string Provide(Context context, string key, string arg) => string.Empty;
    }
}