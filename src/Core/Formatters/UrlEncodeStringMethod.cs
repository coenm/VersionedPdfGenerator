namespace Core.Formatters
{
    using System;
    using System.Web;

    public class UrlEncodeStringMethod : IMethod
    {
        public bool CanHandle(string method)
        {
            return "UrlEncode".Equals(method, StringComparison.InvariantCultureIgnoreCase);
        }

        public string Handle(string method, string arg)
        {
            if (arg == null)
                return arg;

            return HttpUtility.UrlEncode(arg);
        }
    }
}