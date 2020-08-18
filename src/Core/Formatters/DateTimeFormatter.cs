namespace Core.Formatters
{
    using System;
    using System.Globalization;

    public class DateTimeFormatter : IDateTimeFormatter
    {
        private readonly string _formatDateTime;
        private readonly string _formatDate;
        private readonly string _formatTime;

        private DateTimeFormatter()
        {
            _formatDateTime = "yyyy-M-d HH.mm.ss";
            _formatDate = "yyyy-M-d";
            _formatTime = "HH.mm.ss";
        }

        public static DateTimeFormatter Instance { get; } = new DateTimeFormatter();

        public string FormatDateTime(DateTime dateTime, Context context, string format = null)
        {
            var f = _formatDateTime;

            if (!string.IsNullOrWhiteSpace(context.DefaultDateFormats.DateTimeFormat))
                f = context.DefaultDateFormats.DateTimeFormat;

            if (!string.IsNullOrWhiteSpace(format))
                f = format;

            return dateTime.ToString(f, CultureInfo.CurrentUICulture);
        }

        public string FormatDate(DateTime dateTime, Context context, string format = null)
        {
            var f = _formatDate;

            if (!string.IsNullOrWhiteSpace(context.DefaultDateFormats.DateFormat))
                f = context.DefaultDateFormats.DateFormat;

            if (!string.IsNullOrWhiteSpace(format))
                f = format;

            return dateTime.ToString(f, CultureInfo.CurrentUICulture);
        }

        public string FormatTime(DateTime dateTime, Context context, string format = null)
        {
            var f = _formatTime;

            if (!string.IsNullOrWhiteSpace(context.DefaultDateFormats.TimeFormat))
                f = context.DefaultDateFormats.TimeFormat;

            if (!string.IsNullOrWhiteSpace(format))
                f = format;

            return dateTime.ToString(f, CultureInfo.CurrentUICulture);
        }
    }
}