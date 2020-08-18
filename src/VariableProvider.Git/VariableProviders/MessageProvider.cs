namespace VariableProvider.Git.VariableProviders
{
    using System;
    using System.Collections.Generic;

    using Core;
    using LibGit2Sharp;

    internal class MessageProvider : IGitVariableProvider, IGitVariableDescriptor
    {
        private const string KEY = "Message";

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(IRepository repo, Context context, string key, string arg)
        {
            return repo?.Head?.Tip?.Message ?? string.Empty;
        }

        public IEnumerable<GitVariableDescription> Get()
        {
            yield return new GitVariableDescription(KEY, "Commit message.");
        }
    }
}