namespace Core.Formatters
{
    using System;

    public class TrimStartStringMethod : IMethod
    {
        public bool CanHandle(string method)
        {
            return "TrimStart".Equals(method, StringComparison.InvariantCultureIgnoreCase);
        }

        public string Handle(string method, string arg)
        {
            if (arg == null)
                return arg;

            return arg.TrimStart();
        }
    }
}