namespace Core.Formatters
{
    using System;
    using System.Web;

    public class UrlDecodeStringMethod : IMethod
    {
        public bool CanHandle(string method)
        {
            return "UrlDecode".Equals(method, StringComparison.InvariantCultureIgnoreCase);
        }

        public string Handle(string method, string arg)
        {
            if (arg == null)
                return arg;

            return HttpUtility.UrlDecode(arg);
        }
    }
}