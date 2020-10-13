namespace Core.VariableProviders.FileInfo
{
    using System;
    using System.Collections.Generic;

    public class FilePathVariableProvider : IVariableProvider
    {
        private const string KEY = "FilePath";

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return context.FileInfo.DirectoryName;
        }

        public IEnumerable<VariableDescription> Get()
        {
            yield return new VariableDescription(KEY, "File path of the input file.");
        }
    }
}