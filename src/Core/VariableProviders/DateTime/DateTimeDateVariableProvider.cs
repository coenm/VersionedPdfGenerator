﻿namespace Core.VariableProviders.DateTime
{
    using System;
    using System.Collections.Generic;

    using Core.Formatters;

    public class DateTimeDateVariableProvider : IVariableProvider
    {
        private const string KEY = "Date";
        private readonly IDateTimeFormatter _formatter;

        public DateTimeDateVariableProvider(IDateTimeFormatter formatter)
        {
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.InvariantCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return _formatter.FormatDate(context.Now, context, arg);
        }

        public IEnumerable<VariableDescription> Get()
        {
            yield return new VariableDescription(KEY, "Now, formatted as date.");
        }
    }
}