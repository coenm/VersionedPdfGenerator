namespace Core.VariableProviders.FileInfo
{
    using System;
    using System.IO;

    public class FilenameBaseVariableProvider : IVariableProvider
    {
        private const string KEY = "FilenameBase";
        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return Path.GetFileNameWithoutExtension(context.FileInfo.FullName);
        }
    }
}