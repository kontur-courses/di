using System;
using System.Linq;
using CommandLine;
using TagCloud;
using TagCloud.configurations;

namespace TagCloudConsoleUI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = TagCloudBuilder.Create();
            while (!args.Contains("exit"))
            {
                Parser.Default.ParseArguments<TextOptions, DrawOptions>(args)
                    .MapResult<DrawOptions, TextOptions, TagCloudBuilder>(
                        opts =>
                            builder.SetImageConfiguration(
                                    new ImageConfiguration(opts.BackgroundColor, opts.Size.Width, opts.Size.Height)
                                )
                                .SetImageSaveConfiguration(
                                    new ImageSaveConfiguration(opts.FilePath, opts.Format)
                                )
                                .SetTagRepositoryConfiguration(
                                    new TagRepositoryRepositoryConfiguration(
                                        opts.WordColor,
                                        opts.FontFamily,
                                        opts.FontSize)
                                )
                                .Build()
                                .Run(),
                        opts => builder.SetInputFilePath(opts.FilePath),
                        errors => null);
                args = Console.ReadLine()!.Split();
            }
        }
    }
}