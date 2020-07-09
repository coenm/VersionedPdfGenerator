namespace Core.VariableProviders
{
    using System.Collections.Generic;

    public interface IVariableDescriptor
    {
        IEnumerable<VariableDescription> Get();
    }
}