namespace VariableProvider.Git.VariableProviders
{
    using System;
    using System.Collections.Generic;

    using Core;
    using LibGit2Sharp;

    internal class MessageShortProvider : IGitVariableProvider
    {
        private const string KEY = "MessageShort";

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(IRepository repo, Context context, string key, string arg)
        {
            return repo?.Head?.Tip?.MessageShort ?? string.Empty;
        }

        public IEnumerable<GitVariableDescription> Get()
        {
            yield return new GitVariableDescription(KEY, "The short commit message which is usually the first line of the commit.");
        }
    }
}