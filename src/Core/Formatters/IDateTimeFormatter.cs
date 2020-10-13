namespace Core.Formatters
{
    using System;

    public interface IDateTimeFormatter
    {
        string FormatDateTime(DateTime dateTime, Context context, string format = null);

        string FormatDate(DateTime dateTime, Context context, string format = null);

        string FormatTime(DateTime dateTime, Context context, string format = null);
    }
}
