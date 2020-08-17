namespace Core
{
    using System.Collections.Generic;

    using Core.VariableProviders;

    public interface IVariableProviderFactory
    {
        IEnumerable<IVariableProvider> CreateVariableProviders();
    }
}