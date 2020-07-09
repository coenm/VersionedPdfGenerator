namespace Core.VariableProviders
{
    using System;
    using System.IO;

    public class PathSeparatorVariableProvider : IVariableProvider
    {
        private const string KEY = "PathSeparator";

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return new string(Path.DirectorySeparatorChar, 1);
        }
    }
}