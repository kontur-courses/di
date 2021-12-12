using System;
using System.Linq;
using CommandLine;
using TagCloud;
using TagCloud.Drawing;
using TagCloud.TextProcessing;

namespace TagCloud_ConsoleUI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = new TagCloudBuilder();
            var tagCloud = builder
                .GetDefault()
                .WithStatusWriter<ConsoleStatusWriter>()
                .Build();

            while (!args.Contains("exit"))
            {
                Parser.Default.ParseArguments<DrawerOptions, TextProcessingOptions, ClearOptions>(args)
                    .MapResult(
                        (DrawerOptions opts) => tagCloud.DrawTagClouds(opts),
                        (TextProcessingOptions opts) => tagCloud.ProcessText(opts),
                        (ClearOptions opts) => tagCloud.ClearProcessedTexts(),
                        errors => null);
                args = Console.ReadLine().Split();
            }
        }
    }
}