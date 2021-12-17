using System;
using System.Linq;
using CommandLine;
using CommandLine.Text;
using TagCloud;

namespace TagCloud_ConsoleUI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = new TagCloudBuilder();
            var tagCloud = builder
                .CreateDefault()
                .WithStatusWriter<ConsoleStatusWriter>()
                .Build();

            while (!args.Contains("exit"))
            {
                var parserResult = new Parser(c => c.HelpWriter = null)
                    .ParseArguments<DrawerOptions, TextProcessingOptions, ClearOptions>(args);
                parserResult.MapResult(
                    (DrawerOptions opts) => tagCloud.DrawTagClouds(opts),
                    (TextProcessingOptions opts) => tagCloud.ProcessText(opts),
                    (ClearOptions opts) => tagCloud.ClearProcessedTexts(),
                    errors => DisplayHelp(parserResult));
                args = Console.ReadLine().Split();
            }
        }

        private static object DisplayHelp(ParserResult<object> parserResult)
        {
            Console.WriteLine(HelpText.AutoBuild(parserResult, help =>
            {
                help.AdditionalNewLineAfterOption = false;
                help.AddEnumValuesToHelpText = true;
                help.Heading = "TagCloud Console UI\n";
                help.Copyright = string.Empty;
                help.AddPreOptionsText("Пример взаимодействия:\n" +
                                       "process -p dataSample.txt\n" +
                                       "draw");
                return help;
            }));
            return null;
        }
    }
}