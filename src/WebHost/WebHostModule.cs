namespace WebHost
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Core;
    using Core.VariableProviders;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Server;
    using Microsoft.Extensions.Hosting;

    public class WebHostModule : IModule
    {
        private IHost _host;

        public Task InitializeAsync()
        {
            _host = CreateHostBuilder(Array.Empty<string>()).Build();

            return Task.CompletedTask;
        }

        public Task StartAsync()
        {
            _  = _host.RunAsync();
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            return _host.StopAsync();
        }

        public void Dispose()
        {
            _host?.Dispose();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }

        public IEnumerable<IVariableProvider> CreateVariableProviders()
        {
            if (_host == null)
                yield break;

            var server = _host.Services.GetService(typeof(IServer)) as IServer;
            yield return new WebHostVariableProvider(server);
        }
    }
}