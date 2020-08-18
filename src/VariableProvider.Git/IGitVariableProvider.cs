namespace VariableProvider.Git
{
    using Core;
    using LibGit2Sharp;

    internal interface IGitVariableProvider
    {
        bool CanProvide(string key);

        string Provide(IRepository repo, Context context, string key, string arg);
    }
}