using System;
using System.Collections.Generic;
using CommandLine;
using TagCloudGenerator.Clients.CmdClient.CommandLineVerbs;
using TagCloudGenerator.GeneratorCore.TagClouds;

namespace TagCloudGenerator.Clients.CmdClient
{
    public class CommandLineClient : IClient
    {
        private readonly string[] args;

        public CommandLineClient(string[] args) => this.args = args;

        public ITagCloudOptions<ITagCloud> GetOptions()
        {
            ITagCloudOptions<ITagCloud> tagCloudOptions = null;

            Parser.Default.ParseArguments<DoubleFontsCloud<CommonWordsCloud>, FourFontsCloud<WebCloud>>(args)
                .WithParsed<DoubleFontsCloud<CommonWordsCloud>>(options => tagCloudOptions = options)
                .WithParsed<FourFontsCloud<WebCloud>>(options => tagCloudOptions = options)
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