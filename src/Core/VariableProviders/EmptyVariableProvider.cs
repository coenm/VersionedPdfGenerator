namespace Core.VariableProviders
{
    using System;

    public class EmptyVariableProvider : IVariableProvider
    {
        public bool CanProvide(string key)
        {
            return "empty".Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return string.Empty;
        }
    }
}