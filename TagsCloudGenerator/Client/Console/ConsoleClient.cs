using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using CommandLine;
using TagsCloudGenerator.Visualizer;

namespace TagsCloudGenerator.Client.Console
{
    public class ConsoleClient : IClient
    {
        public void Run(ICloudGenerator generator)
        {
            Parser
                .Default
                .ParseArguments<Options>(Environment.GetCommandLineArgs())
                .WithParsed(options => Run(generator, options))
                .WithNotParsed(HandleErrors);
        }

        private static void HandleErrors(IEnumerable<Error> errors)
        {
            System.Console.WriteLine("During argument parsing errors was occured:");

            foreach (var error in errors)
            {
                System.Console.WriteLine(error);
            }
        }

        private static void Run(ICloudGenerator generator, Options options)
        {
            var colors = GetColorsByNames(options.Colors);
            var backgroundColor = Color.FromName(options.BackgroundColor);
            var format = GetImageFormat(options.ImageFormat);
            var font = new Font(options.Font, options.FontSize);
            var settings = new ImageSettings(
                options.ImageWidth, options.ImageHeight, 
                backgroundColor, colors,
                format, font);

            generator.Generate(options.Path, options.Output, settings);
        }

        private static List<Color> GetColorsByNames(string names)
        {
            var colors = names.Split(new[] { ", " }, StringSplitOptions.None);

            return colors.Select(Color.FromName).ToList();
        }

        private static ImageFormat GetImageFormat(string extension)
        {
            ImageFormat result = null;
            var info = typeof(ImageFormat).GetProperties().FirstOrDefault(p =>
                p.Name.Equals(extension, StringComparison.InvariantCultureIgnoreCase));
            if (info != null)
            {
                result = info.GetValue(info) as ImageFormat;
            }

            return result;
        }
    }
}