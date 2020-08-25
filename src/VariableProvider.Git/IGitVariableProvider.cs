namespace VariableProvider.Git
{
    using System.Collections.Generic;

    using Core;
    using LibGit2Sharp;
    using VariableProvider.Git.VariableProviders;

    internal interface IGitVariableProvider
    {
        bool CanProvide(string key);

        string Provide(IRepository repo, Context context, string key, string arg);

        IEnumerable<GitVariableDescription> Get();
    }
}