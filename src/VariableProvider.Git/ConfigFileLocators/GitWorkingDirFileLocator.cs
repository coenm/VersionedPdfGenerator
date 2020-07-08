namespace VariableProvider.Git.ConfigFileLocators
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Core.Config;
    using LibGit2Sharp;

    public class GitWorkingDirFileLocator : IDynamicConfigFileLocator
    {
        private readonly string[] _defaultFilename;

        public GitWorkingDirFileLocator(params string[] defaultFilename)
        {
            _defaultFilename = defaultFilename ?? throw new ArgumentNullException(nameof(defaultFilename));
        }

        public IEnumerable<string> Locate(string inputFilename)
        {
            try
            {
                var dir = new FileInfo(inputFilename).Directory.FullName;
                var rootGitDir = Repository.Discover(dir);
                if (rootGitDir == null)
                    return new List<string>(0);

                var repo = new Repository(rootGitDir);
                rootGitDir = repo.Info.WorkingDirectory;

                if (rootGitDir == null)
                    return new List<string>(0);

                return _defaultFilename
                       .Select(filename => Path.Combine(rootGitDir, filename))
                       .ToList();
            }
            catch (Exception)
            {
                // ignore
            }

            return new List<string>(0);
        }
    }
}