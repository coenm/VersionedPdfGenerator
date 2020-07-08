namespace Core.VariableProviders
{
    using System;
    using System.IO;

    public class PathSeparatorVariableProvider : IVariableProvider
    {
        public bool CanProvide(string key)
        {
            return "PathSeparator".Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return new string(Path.DirectorySeparatorChar, 1);
        }
    }
}