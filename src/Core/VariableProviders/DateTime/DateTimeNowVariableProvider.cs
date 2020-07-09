namespace Core.VariableProviders.DateTime
{
    using System;
    using System.Globalization;

    using Core.Formatters;

    public class DateTimeNowVariableProvider : IVariableProvider
    {
        private const string KEY = "Now";
        private readonly IDateTimeFormatter _formatter;

        public DateTimeNowVariableProvider(IDateTimeFormatter formatter)
        {
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        public bool CanProvide(string key)
        {
            return KEY.Equals(key, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Provide(Context context, string key, string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
                return _formatter.FormatDateTime(context.Now);
            return context.Now.ToString(arg, CultureInfo.CurrentUICulture);
        }
    }
}