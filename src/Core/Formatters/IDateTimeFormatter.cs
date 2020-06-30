namespace Core.Formatters
{
    using System;

    public interface IDateTimeFormatter
    {
        string FormatDateTime(DateTime dateTime);

        string FormatDate(DateTime dateTime);

        string FormatTime(DateTime dateTime);
    }
}