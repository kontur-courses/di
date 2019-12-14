using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using CommandLine;
using TagsCloudGenerator.Saver;
using TagsCloudGenerator.Tools;
using TagsCloudGenerator.Visualizer;
using TagsCloudGenerator.WordsHandler.Filters;

namespace TagsCloudGenerator.Client.Console
{
    public class ConsoleClient : IClient
    {
        private readonly IImageSaver saver;
        private readonly ICloudVisualizer visualizer;
        private readonly BoringWordsEjector boringWordsEjector;

        public ConsoleClient(IImageSaver saver, ICloudVisualizer visualizer, BoringWordsEjector boringWordsEjector)
        {
            this.saver = saver;
            this.visualizer = visualizer;
            this.boringWordsEjector = boringWordsEjector;
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
                System.Console.WriteLine(error);
        }

        private void Run(ICloudGenerator generator, Options options)
        {
            if (options.PathToBoringWords != null)
                LoadBoringWords(options.PathToBoringWords);


            var imageSettings = GetImageSettings(options);

            var input = options.Path;

            var extension = PathHelper.GetFileExtension(input);
            var reader = ReaderFactory.GetReaderFor(extension);
            var words = reader.ReadWords(options.Path);
            var cloud = generator.Generate(words, imageSettings.Font);
            var bitmap = visualizer.Draw(cloud, imageSettings);

            saver.Save(bitmap, options.Output, imageSettings.ImageFormat);
        }

        private void LoadBoringWords(string path)
        {
            var extension = PathHelper.GetFileExtension(path);
            var boringWordsReader = ReaderFactory.GetReaderFor(extension);
            var words = new List<string>();

            try
            {
                words = boringWordsReader.ReadWords(path).Select(x => x.Key).ToList();
            }
            catch (FileNotFoundException e)
            {
                System.Console.WriteLine("Fail load file with boring words: " + e.Message);
            }

            boringWordsEjector.AddBoringWords(words);
        }

        private static ImageSettings GetImageSettings(Options options)
        {
            var colors = GetColorsByNames(options.Colors);
            var backgroundColor = Color.FromName(options.BackgroundColor);
            var format = GetImageFormat(PathHelper.GetFileExtension(options.Output));
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
                result = info.GetValue(info) as ImageFormat;


            return result;
        }
    }
}