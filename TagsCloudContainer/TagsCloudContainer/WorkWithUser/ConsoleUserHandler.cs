using CommandLine;
using System;
using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class ConsoleUserHandler : IUserHandler
    {
        private class StandartOptions
        {
            [Value(1, HelpText = "File to take words from")]
            public string File { get; set;  }
            [Option('c', "count", Required = false, Default = int.MaxValue, HelpText = "Count of words in tag cloud")]
            public int MaxCnt { get; set; }
            [Option('f', "format", Required = false, Default = "png", HelpText = "Format of tag cloud file")]
            public string Format { get; set; }
        }

        private readonly string[] args;

        public ConsoleUserHandler(string[] args)
        {
            this.args = args;
        }

        public InputInfo GetInputInfo()
        {
            string fileName = "";
            int maxCnt = int.MaxValue;
            string imageFormat = "png";
            var a = Parser.Default.ParseArguments<StandartOptions>(args).
                WithParsed(opts =>
                {
                    fileName = opts.File;
                    maxCnt = opts.MaxCnt;
                    imageFormat = opts.Format;
                });
            return new InputInfo(fileName, maxCnt, imageFormat);
        }

        public void WriteToUser(IEnumerable<string> messages)
        {
            foreach (var message in messages)
                Console.WriteLine(message);
        }
    }
}