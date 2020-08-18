namespace VariableProvider.Git.VariableProviders.Committer
{
    using System;
    using System.Collections.Generic;

    using Core;
    using Core.Formatters;
    using LibGit2Sharp;

    internal class CommitterWhenProvider : IGitVariableProvider, IGitVariableDescriptor
    {
        private readonly IDateTimeFormatter _dateTimeFormatter;

        public CommitterWhenProvider(IDateTimeFormatter dateTimeFormatter)
        {
            _dateTimeFormatter = dateTimeFormatter ?? throw new ArgumentNullException(nameof(dateTimeFormatter));
        }

        private const string KEY = "Committer.CommitDate";

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(IRepository repo, Context context, string key, string arg)
        {
            var dt = repo?.Head?.Tip?.Committer?.When.DateTime;
            if (dt.HasValue == false)
                return string.Empty;

            return _dateTimeFormatter.FormatDateTime(dt.Value, context, arg);
        }

        public IEnumerable<GitVariableDescription> Get()
        {
            yield return new GitVariableDescription(KEY, "The date when the committer committed.");
        }
    }
}