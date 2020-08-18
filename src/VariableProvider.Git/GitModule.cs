namespace VariableProvider.Git
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Core;
    using Core.Formatters;
    using Core.VariableProviders;

    public class GitModule : IModule
    {
        private readonly IDateTimeFormatter _dateTimeFormatter;

        public GitModule(IDateTimeFormatter dateTimeFormatter)
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
            yield return new GitVariableProviderComposition(_dateTimeFormatter);
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}