namespace Core
{
    using System;
    using System.Threading.Tasks;

    public interface IModule : IDisposable
    {
        Task InitializeAsync();

        Task StartAsync();

        Task StopAsync();
    }
}