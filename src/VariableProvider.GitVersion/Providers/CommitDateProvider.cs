namespace VariableProvider.GitVersion.Providers
{
    using System;

    using Core.Formatters;

    using global::GitVersion;
    using global::GitVersion.OutputVariables;

    public class CommitDateProvider : IGitVersionVariableProvider
    {
        private readonly IDateTimeFormatter _dateTimeFormatter;

        public CommitDateProvider(IDateTimeFormatter dateTimeFormatter)
        {
            _dateTimeFormatter = dateTimeFormatter ?? throw new ArgumentNullException(nameof(dateTimeFormatter));
        }

        public bool CanProvide(SemanticVersion semanticVersion, VersionVariables versionVariables, string key, string arg)
        {
            return "CommitDate".Equals(key, StringComparison.CurrentCultureIgnoreCase) && !(semanticVersion?.BuildMetaData?.CommitDate is null);
        }

        public string Provide(SemanticVersion semanticVersion, VersionVariables versionVariables, string key, string arg)
        {
            // todo
            var dt = semanticVersion.BuildMetaData.CommitDate.DateTime;
            if (string.IsNullOrWhiteSpace(arg))
                return _dateTimeFormatter.FormatDateTime(dt);
            return dt.ToString(arg);
        }
    }
}