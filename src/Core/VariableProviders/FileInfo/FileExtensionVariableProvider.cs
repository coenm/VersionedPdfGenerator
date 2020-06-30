namespace Core.VariableProviders.FileInfo
{
    using System;

    using Core.Formatters;

    public class FileExtensionVariableProvider : IVariableProvider
    {
        private readonly IStringFormatter _stringFormatterComposition;

        public FileExtensionVariableProvider(IStringFormatter stringFormatterComposition)
        {
            _stringFormatterComposition = stringFormatterComposition;
        }

        public bool CanProvide(string key)
        {
            return "fileextension".Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return _stringFormatterComposition.Format(context.FileInfo.Extension, arg);
        }
    }
}