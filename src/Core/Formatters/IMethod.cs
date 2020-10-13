namespace Core.Formatters
{
    public interface IMethod
    {
        bool CanHandle(string method);

        string Handle(string method, string arg);
    }
}
