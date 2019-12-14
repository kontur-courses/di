using System;
using System.Collections.Generic;
using CommandLine;
using TagCloudGenerator.UserInterfaces.CmdClient.CommandLineVerbs;

namespace TagCloudGenerator.UserInterfaces.CmdClient
{
    public static class ArgumentParser
    {
        public static ITagCloudOptions ReadCommandLineOptions(IEnumerable<string> args)
        {
            ITagCloudOptions tagCloudOptions = null;

            Parser.Default.ParseArguments<DoubleFontsCloud, FourFontsCloud>(args)
                .WithParsed<DoubleFontsCloud>(options => tagCloudOptions = options)
                .WithParsed<FourFontsCloud>(options => tagCloudOptions = options)
                .WithNotParsed(HandleParseErrors);

            return tagCloudOptions;
        }

        private static void HandleParseErrors(IEnumerable<Error> errors)
        {
            var exitCode = 0;

            foreach (var error in errors)
            {
                Console.Error.WriteLine(error);
                exitCode = (int)error.Tag;
            }

            Environment.Exit(exitCode);
        }
    }
}