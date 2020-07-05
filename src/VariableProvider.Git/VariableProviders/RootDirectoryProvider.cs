namespace VariableProvider.Git.VariableProviders
{
    using System;

    using LibGit2Sharp;

    internal class RootDirectoryProvider : IGitVariableProvider
    {
        public bool CanProvide(string key)
        {
            return "rootdir".Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(IRepository repo, string key, string arg)
        {
            return repo.Info?.WorkingDirectory ?? string.Empty;
        }
    }
}