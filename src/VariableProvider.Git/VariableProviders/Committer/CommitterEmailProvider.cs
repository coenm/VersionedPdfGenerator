namespace VariableProvider.Git.VariableProviders.Committer
{
    using System;
    using System.Collections.Generic;

    using Core;
    using LibGit2Sharp;

    internal class CommitterEmailProvider : IGitVariableProvider, IGitVariableDescriptor
    {
        private const string KEY = "Committer.Email";

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(IRepository repo, Context context, string key, string arg)
        {
            return repo?.Head?.Tip?.Committer?.Email ?? string.Empty;
        }

        public IEnumerable<GitVariableDescription> Get()
        {
            yield return new GitVariableDescription(KEY, "The email of the committer of the commit.");
        }
    }
}