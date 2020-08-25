namespace Core.Formatters
{
    using System;

    public class TrimEndStringMethod : IMethod
    {
        public bool CanHandle(string method)
        {
            return "TrimEnd".Equals(method, StringComparison.InvariantCultureIgnoreCase);
        }

        public string Handle(string method, string arg)
        {
            if (arg == null)
                return arg;

            return arg.TrimEnd();
        }
    }
}