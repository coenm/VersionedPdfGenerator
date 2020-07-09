namespace Core.VariableProviders.FileInfo
{
    using System;

    public class FilenameVariableProvider : IVariableProvider
    {
        private const string KEY = "Filename";

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return context.FileInfo.Name;
        }
    }
}