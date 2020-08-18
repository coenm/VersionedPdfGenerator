namespace Core.Formatters
{
    using System;

    public class StringFormatter : IStringFormatter
    {
        private StringFormatter()
        {
        }

        public static StringFormatter Instance { get; }  = new StringFormatter();

        public string Format(string input, string arg)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            if (string.IsNullOrWhiteSpace(arg))
                return input;

            if ("lower".Equals(arg, StringComparison.CurrentCultureIgnoreCase))
                return input.ToLower();

            if ("upper".Equals(arg, StringComparison.CurrentCultureIgnoreCase))
                return input.ToUpper();

            if ("trim".Equals(arg, StringComparison.CurrentCultureIgnoreCase))
                return input.Trim();

            return input;
        }
    }
}