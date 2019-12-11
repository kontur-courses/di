using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using CommandLine;
using TagsCloudGenerator.FileReaders;
using TagsCloudGenerator.Saver;
using TagsCloudGenerator.Visualizer;

namespace TagsCloudGenerator.Client.Console
{
    public class ConsoleClient : IClient
    {
        private IFileReader reader;
        private IImageSaver saver;
        private ICloudVisualizer visualizer;

        public ConsoleClient(IFileReader reader, IImageSaver saver, ICloudVisualizer visualizer)
        {
            this.reader = reader;
            this.saver = saver;
            this.visualizer = visualizer;
        }

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

        private void Run(ICloudGenerator generator, Options options)
        {
            var imageSettings = GetImageSettings(options);
            
            var words = reader.ReadWords(options.Path);
            var cloud = generator.Generate(words, imageSettings.Font);
            var bitmap = visualizer.Draw(cloud, imageSettings);
            
            saver.Save(bitmap, options.Output, imageSettings.ImageFormat);
        }

        private static ImageSettings GetImageSettings(Options options)
        {
            var colors = GetColorsByNames(options.Colors);
            var backgroundColor = Color.FromName(options.BackgroundColor);
            var format = GetImageFormat(options.Output.Split('.').Last());
            var font = new Font(options.Font, options.FontSize);

            return new ImageSettings(options.ImageWidth, options.ImageHeight, backgroundColor, colors, format, font);
        }
        private static List<Color> GetColorsByNames(string names)
        {
            var colors = names.Split(new[] {", "}, StringSplitOptions.None);

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