namespace Core.VariableProviders.FileInfo
{
    using System;
    using System.IO;

    public class FilenameBaseVariableProvider : IVariableProvider
    {
        public bool CanProvide(string key)
        {
            return "filenamebase".Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return Path.GetFileNameWithoutExtension(context.FileInfo.FullName);
        }
    }
}