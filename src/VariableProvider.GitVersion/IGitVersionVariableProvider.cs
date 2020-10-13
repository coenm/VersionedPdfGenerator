namespace VariableProvider.GitVersion
{
    using Core;

    using global::GitVersion;
    using global::GitVersion.OutputVariables;

    internal interface IGitVersionVariableProvider
    {
        bool CanProvide(SemanticVersion semanticVersion, VersionVariables versionVariables, Context context, string key, string arg);

        string Provide(SemanticVersion semanticVersion, VersionVariables versionVariables, Context context, string key, string arg);
    }
}