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

        private static string InitialMessage => "Tag Cloud Generator\nWrite \"--help\" to see commands:";
        private static string BorderString(int width) => new string('=', width);
        private static string SuccessfulMessage(string path) => $"&&&&&&  Image created successful, saved to {path}  &&&&&&";
        private static string ErrorMessage => $"####### Image created unsuccessful, reasons: #######";
        private static string ParseCommandErrorMessage => $"####### Incorrect command: #######";
        private static string ErrorSymbol => "*\t";



        public static void Run(Func<BuildOptions, IContainer> presetBuilder)
        {
            var commandParser = InitCommandParser();
            Console.WriteLine(InitialMessage);

            while (true)
            {
                Console.WriteLine('\n' + BorderString(Console.WindowWidth));
                try
                {
                    var command = Console.ReadLine()?.Split(' ');
                    Console.WriteLine();

                    commandParser.ParseArguments<BuildOptions>(command)
                        .WithParsed(options =>
                        {
                            CloudBuilder.CreateCloudImageAndSave(options, presetBuilder(options));
                            Console.WriteLine(SuccessfulMessage(options.OutputFileName));
                        })
                        .WithNotParsed(errors =>
                        {
                            Console.WriteLine(ParseCommandErrorMessage);
                            foreach (var error in errors)
                                Console.WriteLine(ErrorSymbol + error);
                        });
                }
                catch (Exception e)
                {
                    Console.WriteLine(ErrorMessage);
                    Console.WriteLine(e);
                }
            }

        }
    }
}