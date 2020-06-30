namespace Core.VariableProviders.DateTime
{
    using System;

    using Core.Formatters;

    public class DateTimeDateVariableProvider : IVariableProvider
    {
        private readonly IDateTimeFormatter _formatter;

        public DateTimeDateVariableProvider(IDateTimeFormatter formatter)
        {
            _formatter = formatter;
        }

        public bool CanProvide(string key)
        {
            return "date".Equals(key, StringComparison.InvariantCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            return _formatter.FormatDate(context.Now);
        }
    }
}