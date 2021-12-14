using System;
using System.Collections.Generic;
using CommandLine;
using TagsCloudVisualization.Commands;
using TagsCloudVisualization.Processors;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions, CreateCloudCommand, ShowDemoCommand>(args)
                .MapResult(
                    (CreateCloudCommand options) => CreateCloudProcessor.Run(options),
                    (ShowDemoCommand options) => ShowDemoProcessor.Run(options),
                    HandleParseError);
        }

        private static int HandleParseError(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
                Console.WriteLine(error.ToString());

            return 1;
        }
    }
}