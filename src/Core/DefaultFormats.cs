namespace Core
{
    public readonly struct DefaultFormats
    {
        public DefaultFormats(string dateTimeFormat, string dateFormat, string timeFormat)
        {
            DateTimeFormat = dateTimeFormat;
            DateFormat = dateFormat;
            TimeFormat = timeFormat;
        }

        public string DateTimeFormat { get; }

        public string DateFormat { get; }

        public string TimeFormat { get; }
    }
}