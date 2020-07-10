namespace Core.VariableProviders.FileInfo
{
    using System;
    using System.Collections.Generic;

    using Core.Formatters;

    public class FileExtensionVariableProvider : IVariableProvider, IVariableDescriptor
    {
        private const string KEY = "FileExtension";
        private readonly IStringFormatter _stringFormatterComposition;

        public FileExtensionVariableProvider(IStringFormatter stringFormatterComposition)
        {
            _stringFormatterComposition = stringFormatterComposition;
        }

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return _stringFormatterComposition.Format(context.FileInfo.Extension, arg);
        }

        public IEnumerable<VariableDescription> Get()
        {
            yield return new VariableDescription(KEY, "Extension (including the . (dot)) of the input file.");
        }
    }
}