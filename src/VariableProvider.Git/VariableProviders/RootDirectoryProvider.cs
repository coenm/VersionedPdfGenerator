namespace VariableProvider.Git.VariableProviders
{
    using System;
    using System.Collections.Generic;

    using Core;
    using LibGit2Sharp;

    internal class RootDirectoryProvider : IGitVariableProvider, IGitVariableDescriptor
    {
        private const string KEY = "RootDir";

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(IRepository repo, Context context, string key, string arg)
        {
            return repo.Info?.WorkingDirectory ?? string.Empty;
        }

        public IEnumerable<GitVariableDescription> Get()
        {
            yield return new GitVariableDescription(KEY, "Git working directory");
        }
    }
}