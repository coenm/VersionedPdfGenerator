namespace VariableProvider.GitVersion.Providers
{
    using System.Collections.Generic;

    internal interface IGitVersionVariableDescriptor
    {
        IEnumerable<GitVersionVariableDescription> Get();
    }
}