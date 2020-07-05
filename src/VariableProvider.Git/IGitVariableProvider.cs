namespace VariableProvider.Git
{
    using LibGit2Sharp;

    internal interface IGitVariableProvider
    {
        bool CanProvide(string key);

        string Provide(IRepository repo, string key, string arg);
    }
}