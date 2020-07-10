namespace Core.VariableProviders.DateTime
{
    using System;
    using System.Collections.Generic;

    using Core.Formatters;

    public class DateTimeTimeVariableProvider : IVariableProvider, IVariableDescriptor
    {
        private const string KEY = "Time";
        private readonly IDateTimeFormatter _formatter;

        public DateTimeTimeVariableProvider(IDateTimeFormatter formatter)
        {
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.InvariantCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return _formatter.FormatTime(context.Now);
        }

        public IEnumerable<VariableDescription> Get()
        {
            yield return new VariableDescription(KEY, "Now, formatted as time.");
        }
    }
}