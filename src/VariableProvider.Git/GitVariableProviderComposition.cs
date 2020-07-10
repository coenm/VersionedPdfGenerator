namespace VariableProvider.Git
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core;
    using Core.Formatters;
    using Core.VariableProviders;
    using LibGit2Sharp;
    using VariableProvider.Git.VariableProviders;
    using VariableProvider.Git.VariableProviders.Author;
    using VariableProvider.Git.VariableProviders.Committer;

    public class GitVariableProviderComposition : IVariableProvider, IVariableDescriptor
    {
        private const string PREFIX = "Git.";

        private readonly List<IGitVariableProvider> _gitProviders;
        private readonly List<IGitVariableDescriptor> _gitVariableDescriptionProviders;

        public GitVariableProviderComposition(IDateTimeFormatter dateTimeFormatter)
        {
            if (dateTimeFormatter == null)
                throw new ArgumentNullException(nameof(dateTimeFormatter));

            _gitProviders = new List<IGitVariableProvider>
                                {
                                    new ShaProvider(),
                                    new MessageProvider(),
                                    new MessageShortProvider(),
                                    new AuthorEmailProvider(),
                                    new AuthorNameProvider(),
                                    new AuthorWhenProvider(dateTimeFormatter),
                                    new CommitterEmailProvider(),
                                    new CommitterNameProvider(),
                                    new CommitterWhenProvider(dateTimeFormatter),
                                    new RootDirectoryProvider(),
                                };

            _gitVariableDescriptionProviders = _gitProviders
                                               .Select(x => x as IGitVariableDescriptor)
                                               .Where(x => x != null)
                                               .ToList();
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

            return _gitProviders.Any(p => p.CanProvide(gitVariableKey));
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
                var provider = _gitProviders.FirstOrDefault(p => p.CanProvide(gitVariableKey));
                return provider is null
                           ? string.Empty
                           : provider.Provide(repo, gitVariableKey, arg);
            }
        }

        public IEnumerable<VariableDescription> Get()
        {
            foreach (var provider in _gitVariableDescriptionProviders)
            {
                foreach (var description in provider.Get())
                {
                    yield return new VariableDescription(PREFIX + description.Key, description.Description);
                }
            }
        }
    }
}
