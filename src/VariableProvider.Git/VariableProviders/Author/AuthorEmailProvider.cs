namespace VariableProvider.Git.VariableProviders.Author
{
    using System;
    using System.Collections.Generic;

    using LibGit2Sharp;

    internal class AuthorEmailProvider : IGitVariableProvider, IGitVariableDescriptor
    {
        private const string KEY = "Author.Email";

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(IRepository repo, string key, string arg)
        {
            return repo?.Head?.Tip?.Author?.Email ?? string.Empty;
        }

        public IEnumerable<GitVariableDescription> Get()
        {
            yield return new GitVariableDescription(KEY, "The email of the author of the commit.");
        }
    }
}