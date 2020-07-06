namespace VariableProvider.GitVersion
{
    using System;
    using global::GitVersion;
    using global::GitVersion.Extensions;
    using global::GitVersion.OutputVariables;
    using global::GitVersion.VersionCalculation;
    using LibGit2Sharp;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    internal static class GitToolsFacade
    {
        internal static (SemanticVersion executeGitVersion, VersionVariables variables) GetVersion(IRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            var gitVersionOptions = new GitVersionOptions
                                        {
                                            WorkingDirectory = repository.Info.WorkingDirectory.TrimEnd('\\'),
                                        };

            var options = Options.Create(gitVersionOptions);

            var serviceProvider = ConfigureServices(services => services.AddSingleton(options));

            var variableProvider = serviceProvider.GetService<IVariableProvider>();
            var nextVersionCalculator = serviceProvider.GetService<INextVersionCalculator>();
            var contextOptions = serviceProvider.GetService<Lazy<GitVersionContext>>();
            var context = contextOptions.Value;

            try
            {
                SemanticVersion executeGitVersion = nextVersionCalculator.FindVersion();
                VersionVariables variables = variableProvider.GetVariablesFor(executeGitVersion, context.Configuration, context.IsCurrentCommitTagged);

                return (executeGitVersion, variables);
            }
            catch (Exception ex)
            {
                var e = ex.Message;
                Console.WriteLine("Test failing, dumping repository graph");
                throw;
            }
        }

        private static IServiceProvider ConfigureServices(Action<IServiceCollection> servicesOverrides = null)
        {
            var services = new ServiceCollection().AddModule(new GitVersionCoreModule());
            servicesOverrides?.Invoke(services);
            return services.BuildServiceProvider();
        }
    }
}
