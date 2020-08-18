namespace VariableProvider.Git.VariableProviders
{
    using System;
    using System.Collections.Generic;

    using Core;
    using LibGit2Sharp;

    internal class ShaProvider : IGitVariableProvider, IGitVariableDescriptor
    {
        private const string KEY = "Sha";

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(IRepository repo, Context context, string key, string arg)
        {
            return repo?.Head?.Tip?.Sha ?? string.Empty;
        }

        public IEnumerable<GitVariableDescription> Get()
        {
            yield return new GitVariableDescription(KEY, "The 40 character sha1 of current commit");
        }
    }
}