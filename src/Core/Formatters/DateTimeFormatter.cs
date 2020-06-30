namespace Core.Formatters
{
    using System;
    using System.Globalization;

    public class ConfigurableDateTimeFormatter : IDateTimeFormatter
    {
        private readonly string _formatDateTime;
        private readonly string _formatDate;
        private readonly string _formatTime;

        public ConfigurableDateTimeFormatter(string formatDateTime, string formatDate, string formatTime)
        {
            _formatDateTime = formatDateTime;
            _formatDate = formatDate;
            _formatTime = formatTime;
        }

        public string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString(_formatDateTime, CultureInfo.CurrentUICulture);
        }

        public string FormatDate(DateTime dateTime)
        {
            return dateTime.ToString(_formatDate, CultureInfo.CurrentUICulture);
        }

        public string FormatTime(DateTime dateTime)
        {
            return dateTime.ToString(_formatTime, CultureInfo.CurrentUICulture);
        }
    }
}