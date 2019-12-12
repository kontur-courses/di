using CommandLine;


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
        }

        static void Main(string[] args)
        {
            var a = Parser.Default.ParseArguments<StandartOptions>(args).
                WithParsed(opts => ProgrammMain.Execute(opts));
        }
    }
}