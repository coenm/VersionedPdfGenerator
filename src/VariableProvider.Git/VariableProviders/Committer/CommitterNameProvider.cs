namespace VariableProvider.Git.VariableProviders.Committer
{
    using System;
    using System.Collections.Generic;

    using Core;
    using LibGit2Sharp;

    internal class CommitterNameProvider : IGitVariableProvider, IGitVariableDescriptor
    {
        private const string KEY = "Committer.Name";

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(IRepository repo, Context context, string key, string arg)
        {
            return repo?.Head?.Tip?.Committer?.Name ?? string.Empty;
        }

        public IEnumerable<GitVariableDescription> Get()
        {
            yield return new GitVariableDescription(KEY, "The name of the committer of the commit.");
        }
    }
}