namespace VariableProvider.Git.VariableProviders
{
    using System.Collections.Generic;

    internal interface IGitVariableDescriptor
    {
        IEnumerable<GitVariableDescription> Get();
    }
}