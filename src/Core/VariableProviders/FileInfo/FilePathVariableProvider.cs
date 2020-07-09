namespace Core.VariableProviders.FileInfo
{
    using System;

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
    }
}