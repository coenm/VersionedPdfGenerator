namespace VariableProvider.GitVersion.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::GitVersion;
    using global::GitVersion.OutputVariables;

    internal class DynamicGitVersionProvider : IGitVersionVariableProvider, IGitVersionVariableDescriptor
    {
        public bool CanProvide(SemanticVersion semanticVersion, VersionVariables versionVariables, string key, string arg)
        {
            if (versionVariables is null)
                return false;

            // this is already captured by other provider using it as DateTime.
            if (nameof(VersionVariables.CommitDate).Equals(key, StringComparison.CurrentCultureIgnoreCase))
                return false;

            var foundKey = VersionVariables.AvailableVariables.FirstOrDefault(item => key.Equals(item, StringComparison.CurrentCultureIgnoreCase));

            return !string.IsNullOrWhiteSpace(foundKey);
        }

        public string Provide(SemanticVersion semanticVersion, VersionVariables versionVariables, string key, string arg)
        {
            var foundKey = VersionVariables.AvailableVariables.FirstOrDefault(item => key.Equals(item, StringComparison.CurrentCultureIgnoreCase));
            return versionVariables[foundKey];
        }

        public IEnumerable<GitVersionVariableDescription> Get()
        {
            foreach (var key in VersionVariables.AvailableVariables.Where(name => !nameof(VersionVariables.CommitDate).Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                yield return new GitVersionVariableDescription(key, "See GitVersion for information.");
            }
        }
    }
}