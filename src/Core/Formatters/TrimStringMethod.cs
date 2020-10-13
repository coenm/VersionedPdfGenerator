namespace Core.Formatters
{
    using System;

    public class TrimStringMethod : IMethod
    {
        public bool CanHandle(string method)
        {
            return "Trim".Equals(method, StringComparison.InvariantCultureIgnoreCase);
        }

        public string Handle(string method, string arg)
        {
            if (arg == null)
                return arg;

            return arg.Trim();
        }
    }
}