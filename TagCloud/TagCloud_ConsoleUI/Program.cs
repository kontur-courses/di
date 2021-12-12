using System;
using System.Linq;
using CommandLine;
using TagCloud;
using TagCloud.Drawing;
using TagCloud.TextProcessing;

namespace TagCloud_ConsoleUI
{
    public class Program
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
                Parser.Default.ParseArguments<DrawerOptions, TextProcessingOptions>(args)
                    .MapResult(
                        (DrawerOptions opts) => tagCloud.DrawTagClouds(opts),
                        (TextProcessingOptions opts) => tagCloud.ProcessText(opts),
                        _ => 1);
                args = Console.ReadLine().Split();
            }
        }
    }
}
