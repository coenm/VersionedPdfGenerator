namespace Core.VariableProviders.FileInfo
{
    using System;

    public class FilenameVariableProvider : IVariableProvider
    {
        public bool CanProvide(string key)
        {
            return "filename".Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return context.FileInfo.Name;
        }
    }
}