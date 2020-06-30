namespace Core.VariableProviders
{
    public interface IVariableProvider
    {
        bool CanProvide(string key);

        string Provide(Context context, string key, string arg);
    }
}