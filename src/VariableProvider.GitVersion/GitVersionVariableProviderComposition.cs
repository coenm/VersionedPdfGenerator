namespace VariableProvider.GitVersion
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Core;
    using Core.Formatters;
    using Core.VariableProviders;
    using LibGit2Sharp;
    using VariableProvider.GitVersion.Providers;

    public class GitVersionVariableProviderComposition : IVariableProvider
    {
        private const string PREFIX = "gitversion.";

        private readonly List<IGitVersionVariableProvider> _providers;

        public GitVersionVariableProviderComposition(ConfigurableDateTimeFormatter dateTimeFormatter)
        {
            if (dateTimeFormatter == null)
                throw new ArgumentNullException(nameof(dateTimeFormatter));

            _providers = new List<IGitVersionVariableProvider>
                            {
                                new CommitDateProvider(dateTimeFormatter),
                                new DynamicGitVersionProvider(),
                            };
        }

        public bool CanProvide(string key)
        {
            if (key is null)
                return false;
            if (!key.StartsWith(PREFIX, StringComparison.CurrentCultureIgnoreCase))
                return false;

            var prefixLength = PREFIX.Length;
            if (key.Length <= prefixLength)
                return false;

            var gitVariableKey = key.Substring(prefixLength, key.Length - prefixLength);
            return gitVariableKey.Length > 0;
        }

        public string Provide(Context context, string key, string arg)
        {
            var prefixLength = PREFIX.Length;
            var gitVariableKey = key.Substring(prefixLength, key.Length - prefixLength);

            var rootGitDir = Repository.Discover(context.FileInfo.DirectoryName);
            if (string.IsNullOrWhiteSpace(rootGitDir))
                return string.Empty;

            using (IRepository repo = new Repository(rootGitDir))
            {
                var versionInfo = GitToolsFacade.GetVersion(repo);
                var provider = _providers.FirstOrDefault(p => p.CanProvide(versionInfo.executeGitVersion, versionInfo.variables, gitVariableKey, arg));
                return provider is null
                           ? string.Empty
                           : provider.Provide(versionInfo.executeGitVersion, versionInfo.variables, gitVariableKey, arg);
            }
        }
    }
}
