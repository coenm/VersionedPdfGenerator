namespace Core.VariableProviders.FileInfo
{
    using System;
    using System.Collections.Generic;

    public class FilenameVariableProvider : IVariableProvider, IVariableDescriptor
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

        public IEnumerable<VariableDescription> Get()
        {
            yield return new VariableDescription(KEY, "Filename of the input file (without the path).");
        }
    }
}