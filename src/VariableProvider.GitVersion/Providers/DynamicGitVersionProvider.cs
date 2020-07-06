namespace VariableProvider.GitVersion.Providers
{
    using System;
    using System.Linq;

    using global::GitVersion;
    using global::GitVersion.OutputVariables;

    public class DynamicGitVersionProvider : IGitVersionVariableProvider
    {
        public bool CanProvide(SemanticVersion semanticVersion, VersionVariables versionVariables, string key, string arg)
        {
            if (versionVariables is null)
                return false;

            var foundKey = VersionVariables.AvailableVariables.FirstOrDefault(item => key.Equals(item, StringComparison.CurrentCultureIgnoreCase));

            return !string.IsNullOrWhiteSpace(foundKey);
        }

        public string Provide(SemanticVersion semanticVersion, VersionVariables versionVariables, string key, string arg)
        {
            var foundKey = VersionVariables.AvailableVariables.FirstOrDefault(item => key.Equals(item, StringComparison.CurrentCultureIgnoreCase));
            return versionVariables[foundKey];
        }
    }
}