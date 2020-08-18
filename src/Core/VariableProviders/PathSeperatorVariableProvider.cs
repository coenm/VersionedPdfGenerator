namespace Core.VariableProviders
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class PathSeparatorVariableProvider : IVariableProvider
    {
        private const string KEY = "PathSeparator";
        private static readonly string PathSeparator = new string(Path.DirectorySeparatorChar, 1);

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return PathSeparator;
        }

        public IEnumerable<VariableDescription> Get()
        {
            yield return new VariableDescription(KEY, $"Path separator ({PathSeparator})");
        }
    }
}