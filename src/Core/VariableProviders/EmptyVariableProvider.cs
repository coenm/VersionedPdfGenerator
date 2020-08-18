namespace Core.VariableProviders
{
    using System;
    using System.Collections.Generic;

    public class EmptyVariableProvider : IVariableProvider, IVariableDescriptor
    {
        private const string KEY = "empty";

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return string.Empty;
        }

        public IEnumerable<VariableDescription> Get()
        {
            yield return new VariableDescription(KEY, "Empty string.");
        }
    }
}