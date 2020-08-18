namespace Core.VariableProviders
{
    using System.Collections.Generic;

    public interface IVariableProvider
    {
        bool CanProvide(string key);

        string Provide(Context context, string key, string arg);

        IEnumerable<VariableDescription> Get();
    }
}