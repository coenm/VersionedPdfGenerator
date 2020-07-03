namespace PdfGenerator
{
    using System;
    using System.Collections.Generic;

    using PdfGenerator.CommandLineOptions;
    using PdfGenerator.CommandLineOptions.CommandHandlers;

    public class Program
    {
        static void Main(string[] args)
        {
            var commandLineCommandHandlers = new List<ICommandLineCommandHandler>
                                                 {
                                                     new CreateOptionsCommandHandler(),
                                                     new GenerateConfigOptionsCommandHandler(),
                                                 };

            var compositeCommandLineCommandHandler = new CommandLineCommandHandlerComposition(commandLineCommandHandlers);

            try
            {
                var command = CommandLineParser.Parse(args);

                compositeCommandLineCommandHandler.Handle(command);

                Console.WriteLine("Done.Press enter to exit.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred. Press enter to exit.");
                Console.WriteLine(e);
            }
        }
    }
}
