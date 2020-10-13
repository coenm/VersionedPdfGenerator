namespace Core
{
    using System;
    using System.Threading.Tasks;

    public interface IModule : IVariableProviderFactory, IDisposable
    {
        Task InitializeAsync();

        Task StartAsync();

        Task StopAsync();
    }
}