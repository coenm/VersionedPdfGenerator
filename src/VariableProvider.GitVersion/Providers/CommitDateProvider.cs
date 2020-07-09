namespace VariableProvider.GitVersion.Providers
{
    using System;
    using System.Collections.Generic;

    using Core.Formatters;
    using global::GitVersion;
    using global::GitVersion.OutputVariables;

    internal class CommitDateProvider : IGitVersionVariableProvider, IGitVersionVariableDescriptor
    {
        private const string KEY = "CommitDate";
        private readonly IDateTimeFormatter _dateTimeFormatter;

        public CommitDateProvider(IDateTimeFormatter dateTimeFormatter)
        {
            _dateTimeFormatter = dateTimeFormatter ?? throw new ArgumentNullException(nameof(dateTimeFormatter));
        }

        public bool CanProvide(SemanticVersion semanticVersion, VersionVariables versionVariables, string key, string arg)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase) && !(semanticVersion?.BuildMetaData?.CommitDate is null);
        }

        public string Provide(SemanticVersion semanticVersion, VersionVariables versionVariables, string key, string arg)
        {
            // todo
            var dt = semanticVersion.BuildMetaData.CommitDate.DateTime;
            if (string.IsNullOrWhiteSpace(arg))
                return _dateTimeFormatter.FormatDateTime(dt);
            return dt.ToString(arg);
        }

        public IEnumerable<GitVersionVariableDescription> Get()
        {
            yield return new GitVersionVariableDescription(KEY, "Commit DateTime of current Head");
        }
    }
}