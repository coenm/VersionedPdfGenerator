namespace VariableProvider.Git.VariableProviders.Author
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Core.Formatters;
    using LibGit2Sharp;

    internal class AuthorWhenProvider : IGitVariableProvider, IGitVariableDescriptor
    {
        private readonly IDateTimeFormatter _dateTimeFormatter;

        public AuthorWhenProvider(IDateTimeFormatter dateTimeFormatter)
        {
            _dateTimeFormatter = dateTimeFormatter ?? throw new ArgumentNullException(nameof(dateTimeFormatter));
        }

        private const string KEY = "Author.CommitDate";

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(IRepository repo, string key, string arg)
        {
            var dt = repo?.Head?.Tip?.Author?.When.DateTime;
            if (dt.HasValue == false)
                return string.Empty;

            if (string.IsNullOrWhiteSpace(arg))
                return _dateTimeFormatter.FormatDateTime(dt.Value);

            return dt.Value.ToString(arg, CultureInfo.CurrentUICulture);
        }

        public IEnumerable<GitVariableDescription> Get()
        {
            yield return new GitVariableDescription(KEY, "The date when the author committed.");
        }
    }
}