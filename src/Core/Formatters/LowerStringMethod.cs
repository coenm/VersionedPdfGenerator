namespace Core.Formatters
{
    using System;

    public class LowerStringMethod : IMethod
    {
        public bool CanHandle(string method)
        {
            return "Lower".Equals(method, StringComparison.InvariantCultureIgnoreCase);
        }

        public string Handle(string method, string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
                return arg;

            return arg.ToLower();
        }
    }
}