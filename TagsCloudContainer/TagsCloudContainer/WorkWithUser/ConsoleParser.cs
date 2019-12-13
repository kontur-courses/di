using CommandLine;
using System;

namespace TagsCloudContainer
{
    public class ConsoleParser
    {
        public class StandartOptions
        {
            [Value(1, HelpText = "File to take words from")]
            public string File { get; set;  }
            [Option('c', "count", Required = false, Default = int.MaxValue, HelpText = "Count of words in tag cloud")]
            public int MaxCnt { get; set; }
            [Option('f', "format", Required = false, Default = "png", HelpText = "Format of tag cloud file")]
            public string Format { get; set; }
        }

        static void Main(string[] args)
        {
            var a = Parser.Default.ParseArguments<StandartOptions>(args).
                WithParsed(opts => ProgrammMain.Execute(opts));
            var logs = OutputLogger.GetAllLogs();
            foreach (var log in logs)
                Console.WriteLine(log);
        }
    }
}