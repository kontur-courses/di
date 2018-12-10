using System;
using System.Collections.Generic;
using System.Drawing;
using CommandLine;
using TagsCloudContainer.ImageCreators;
using TagsCloudContainer.ImageSavers;
using TagsCloudContainer.Readers;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordsHandlers;

namespace TagsCloudContainer.Clients
{
    public class ConsoleClient : IClient
    {
        private class Options
        {
            [Option('h', "height", Required = true, HelpText = "Set height of image")]
            public int Height { get; set; }

            [Option('w', "weight", Required = false, HelpText = "Set width of image")]
            public int Width { get; set; }

            [Option('f', "font", Required = true, HelpText = "Font of text")]
            public string Font { get; set; }

            [Option('t', "text_path", Required = true, HelpText = "Source path of words")]
            public string Source { get; set; }

            [Option('p', "picture_path", Required = true, HelpText = "Destination path of tags cloud")]
            public string Destination { get; set; }

            [Option('c', "text_color", Required = true, HelpText = "Color of text")]
            public string Color { get; set; }
        }

        private readonly ImageCreator imageCreator;
        private readonly WordsHandler wordsHandler;
        private readonly IImageSaver imageSaver;
        private readonly Func<string, IEnumerable<string>> filesReaderFactory;
        private readonly SettingsManager settings;

        public ConsoleClient(WordsHandler wordsHandler, ImageCreator imageCreator, Func<string, IEnumerable<string>> filesReaderFactory,
            SettingsManager settings, IImageSaver imageSaver)
        {
            this.wordsHandler = wordsHandler;
            this.imageCreator = imageCreator;
            this.imageSaver = imageSaver;
            this.filesReaderFactory = filesReaderFactory;
            this.settings = settings;
        }



        public void Execute(string[] args)
        {
            Parser.Default.ParseArguments<Program.Options>(args)
                .WithParsed(o =>
                {
                    UpdateSettings(settings, o);
                    var words = new TextFileReader(o.Source);
                    var transformedWord = wordsHandler.HandleWords(words);
                    var image = imageCreator.GetImage(transformedWord);
                    imageSaver.SaveImage(image, o.Destination);
                    Console.WriteLine("Image successfully created");
                });
        }

        private static void UpdateSettings(SettingsManager settings, Program.Options options)
        {
            var size = new Size(options.Width, options.Height);
            RaiseIsSizeIsIncorrect(size);
            settings.ImageSettings.ImageSize = size;
            UpdateFont(settings, options.Font);
            UpdateColor(settings, options.Color);
        }

        private static void RaiseIsSizeIsIncorrect(Size size)
        {
            if (size.Height <= 0 || size.Height <= 0)
                throw new ArgumentException("Size should has positive height and width");
        }

        private static void UpdateFont(SettingsManager settings, string font)
        {
            try
            {
                settings.TextSettings.Family = new FontFamily(font);
            }
            catch (Exception exception)
            {
                throw new ArgumentException("Font is incorrect");
            }
        }

        private static void UpdateColor(SettingsManager setting, string colorName)
        {
            var color = Color.FromName(colorName);
            if (color.IsKnownColor)
                setting.Palette.FontColor = color;
            else
                throw new ArgumentException($"Font color ({colorName}) is unknown");
        }

    }
}