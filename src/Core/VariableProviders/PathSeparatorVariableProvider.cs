namespace Core.VariableProviders
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class PathSeparatorVariableProvider : IVariableProvider
    {
        private const string KEY = "PathSeparator";
        private static readonly string _pathSeparator = new string(Path.DirectorySeparatorChar, 1);

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return _pathSeparator;
        }

        public IEnumerable<VariableDescription> Get()
        {
            yield return new VariableDescription(KEY, $"Path separator. Current value is '{_pathSeparator}'.");
        }
    }
}
