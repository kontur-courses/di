using System;
using System.Drawing.Imaging;
using System.IO;
using CommandLine;
using ResultProject;
using TagsCloudVisualization;

namespace TagCloudUsageSample
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<ClTextOptions>(args)
                .WithParsed(options =>
                { 
                    options.AsResult()
                        .Then(x => x.GetConfig())
                        .Then(config => (config, Path.Combine(config.SavePath, config.SaveFileName) + "." + ImageFormat.Png.ToString().ToLower()))
                        .Then(x => (x.config, x.Item2, new TagCloud()))
                        .Then(x => (x.Item2, x.Item3.GetBitmap(x.config), x.config.TagCloudResultActions))
                        .Then(x => (x.Item1, x.Item2.GetValueOrThrow(), x.TagCloudResultActions))
                        .ThenAction(x =>
                        {
                            if (x.TagCloudResultActions is TagCloudResultActions.Save or TagCloudResultActions.SaveAndOpen)
                                x.Item2.Save(x.Item1, ImageFormat.Png);
                        })
                        .ThenAction(x => TryOpen(x.Item1, x.TagCloudResultActions))
                        .OnFail(Console.WriteLine);
                });
        }
        
        private static void TryOpen(string firstFileName, TagCloudResultActions tagCloudResultActions)
        {
            if (firstFileName is not null && tagCloudResultActions == TagCloudResultActions.SaveAndOpen)
                System.Diagnostics.Process.Start(firstFileName);
        }
    }
}