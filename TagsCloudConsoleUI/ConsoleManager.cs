using System;
using Autofac;
using CommandLine;

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

        public static void Run(Func<BuildOptions, IContainer> presetBuilder, IConsoleManagerFormatter formatter)
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
                            CloudBuilder.CreateCloudImageAndSave(options, presetBuilder(options));
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