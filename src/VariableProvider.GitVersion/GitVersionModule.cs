namespace VariableProvider.GitVersion
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Core;
    using Core.Formatters;
    using Core.VariableProviders;

    public class GitVersionModule : IModule
    {
        private readonly IDateTimeFormatter _dateTimeFormatter;

        public GitVersionModule(IDateTimeFormatter dateTimeFormatter)
        {
            _dateTimeFormatter = dateTimeFormatter ?? throw new ArgumentNullException(nameof(dateTimeFormatter));
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task StartAsync()
        {
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }

        public IEnumerable<IVariableProvider> CreateVariableProviders()
        {
            yield return new GitVersionVariableProviderComposition(_dateTimeFormatter);
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}