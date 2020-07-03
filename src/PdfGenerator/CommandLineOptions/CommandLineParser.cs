namespace PdfGenerator.CommandLineOptions
{
    using System;
    using System.Linq;
    using System.Reflection;

    using CommandLine;

    using PdfGenerator.CommandLineOptions.Verbs;

    public static class CommandLineParser
    {
        public static ICommandLineCommand Parse(params string[] args)
        {
            var commandLineVerbs = FindCommandLineVerbs();

            ICommandLineCommand result = null;

            Parser.Default.ParseArguments(args, commandLineVerbs)
                       .WithParsed(options =>
                                   {
                                       if (options is ICommandLineCommand argCommand)
                                           result = argCommand;
                                   })
                       .WithNotParsed(errs => throw new Exception(errs.ToString()));

            return result;
        }

        private static Type[] FindCommandLineVerbs()
        {
            // todo and implements the interface
            return typeof(ICommandLineCommand)
                   .Assembly
                   .GetTypes()
                   .Where(t => t.GetCustomAttribute<VerbAttribute>() != null)
                   .ToArray();
        }
    }
}