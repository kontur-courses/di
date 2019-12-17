using CommandLine;
using System;

namespace TagsCloudConsoleUI
{
    internal static class ConsoleManager
    {
        private static Parser InitCommandParser()
        {
            return new Parser(config =>
            {
                config.HelpWriter = Console.Out;
                config.EnableDashDash = true;
            });
        }

        public static void Run(IConsoleManagerFormatter formatter, Action<BuildOptions> onCallAction)
        {
            var commandParser = InitCommandParser();
            Console.WriteLine(formatter.InitialMessage);

            while (true)
            {
                Console.WriteLine('\n' + formatter.BorderString(Console.WindowWidth));
                try
                {
                    var command = Console.ReadLine()?.Split(' ');
                    Console.WriteLine();

                    commandParser.ParseArguments<BuildOptions>(command)
                        .WithParsed(options =>
                        {
                            onCallAction(options);
                            Console.WriteLine(formatter.SuccessfulMessage(options.OutputFileName));
                        })
                        .WithNotParsed(errors =>
                        {
                            Console.WriteLine(formatter.ParseCommandErrorMessage);
                            foreach (var error in errors)
                                Console.WriteLine(formatter.ErrorSymbol + error);
                        });
                }
                catch (Exception e)
                {
                    Console.WriteLine(formatter.ErrorMessage);
                    Console.WriteLine(e);
                }
            }

        }
    }
}