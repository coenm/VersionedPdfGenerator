namespace Core.VariableProviders.FileInfo
{
    using System;

    public class FilePathVariableProvider : IVariableProvider
    {
        public bool CanProvide(string key)
        {
            return "filepath".Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return context.FileInfo.DirectoryName;
        }
    }
}