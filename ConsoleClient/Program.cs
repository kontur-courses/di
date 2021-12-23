using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Autofac;
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
                        .Then(HandleConfig)
                        .OnFail(Console.WriteLine);
                });
        }

        private static Result<Bitmap> HandleConfig(Config config)
        {
           return AutofacConfigurator.InjectWith<TagCloud>().GetBitmap(config)
                .ThenAction(bmp =>
                {
                    if (config.TagCloudResultActions is TagCloudResultActions.Save or TagCloudResultActions.SaveAndOpen)
                        bmp.Save(GetPath(config), ImageFormat.Png);
                })
                .ThenAction(_ => TryOpen(GetPath(config), config.TagCloudResultActions));
        }

        private static string GetPath(Config config)
        {
            return Path.Combine(config.SavePath, config.SaveFileName) + "." + ImageFormat.Png.ToString().ToLower();
        }
        
        private static void TryOpen(string firstFileName, TagCloudResultActions tagCloudResultActions)
        {
            if (firstFileName is not null && tagCloudResultActions == TagCloudResultActions.SaveAndOpen)
                System.Diagnostics.Process.Start(firstFileName);
        }
    }
}