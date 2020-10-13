namespace Core.Formatters
{
    using System;

    public class UpperStringMethod : IMethod
    {
        public bool CanHandle(string method)
        {
            return "Upper".Equals(method, StringComparison.InvariantCultureIgnoreCase);
        }

        public string Handle(string method, string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
                return arg;

            return arg.ToUpper();
        }
    }
}