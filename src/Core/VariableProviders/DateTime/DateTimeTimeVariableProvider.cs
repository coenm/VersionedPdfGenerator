namespace Core.VariableProviders.DateTime
{
    using System;

    using Core.Formatters;

    public class DateTimeTimeVariableProvider : IVariableProvider
    {
        private readonly IDateTimeFormatter _formatter;

        public DateTimeTimeVariableProvider(IDateTimeFormatter formatter)
        {
            _formatter = formatter;
        }

        public bool CanProvide(string key)
        {
            return "time".Equals(key, StringComparison.InvariantCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return _formatter.FormatTime(context.Now);
        }
    }
}