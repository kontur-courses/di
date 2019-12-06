using CommandLine;
using System;


namespace TagsCloudContainer
{
    public class ConsoleParser
    {
        public class StandartOptions
        {
            [Value(0, HelpText = "File to take words from")]
            public string File { get; set;  }
        }

        static void Main(string[] args) //TODO It does not work now on my computer(parsing)
        {
            //args = new string[] { "D:\\ШПОРА\\6_DI\\Homework\\di\\TagsCloudContainer\\TagsCloudContainer\\abc.txt" };
            var a = Parser.Default.ParseArguments<StandartOptions>(args).
                WithParsed(opts => ProgrammMain.Execute(opts));
        }
    }
}