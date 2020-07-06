namespace VariableProvider.GitVersion
{
    using global::GitVersion;
    using global::GitVersion.OutputVariables;

    internal interface IGitVersionVariableProvider
    {
        bool CanProvide(SemanticVersion semanticVersion, VersionVariables versionVariables, string key, string arg);

        string Provide(SemanticVersion semanticVersion, VersionVariables versionVariables, string key, string arg);
    }
}