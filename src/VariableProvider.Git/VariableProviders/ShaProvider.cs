namespace VariableProvider.Git.VariableProviders
{
    using System;

    using LibGit2Sharp;

    internal class ShaProvider : IGitVariableProvider
    {
        public bool CanProvide(string key)
        {
            return "sha".Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(IRepository repo, string key, string arg)
        {
            return repo?.Head?.Tip?.Sha ?? string.Empty;
        }
    }
}