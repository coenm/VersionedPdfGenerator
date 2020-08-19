namespace Core.Formatters
{
    using System;
    using System.Web;

    public interface IMethod
    {
        bool CanHandle(string method);

        string Handle(string method, string arg);
    }

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