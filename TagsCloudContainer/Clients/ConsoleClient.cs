using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommandLine;
using TagsCloudContainer.ImageCreators;
using TagsCloudContainer.ImageSavers;
using TagsCloudContainer.ProjectSettings;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordsHandlers;

namespace TagsCloudContainer.Clients
{
    public class ConsoleClient : IClient
    {
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
            Parser.Default.ParseArguments<ConsoleClientOptions>(args)
                .WithParsed(o =>
                {
                    UpdateSettings(o);
                    var words = ReadFile(o.Source);
                    var transformedWord = wordsHandler.HandleWords(words);
                    var image = imageCreator.GetImage(transformedWord);
                    SaveImage(image, o.Destination);
                    Console.WriteLine("Image successfully created");
                });
        }

        private void UpdateSettings(ConsoleClientOptions consoleClientOptions)
        {
            var size = new Size(consoleClientOptions.Width, consoleClientOptions.Height);
            RaiseIfSizeIsHasNonPositiveCoordinates(size);
            settings.ImageSettings.ImageSize = size;
            UpdateFont(consoleClientOptions.Font);
            UpdateColor(consoleClientOptions.Color);
        }

        private void RaiseIfSizeIsHasNonPositiveCoordinates(Size size)
        {
            if (size.Height <= 0 || size.Height <= 0)
                FailApplication("Size should has positive height and width");
        }

        private IEnumerable<string> ReadFile(string fileName)
        {
            try
            {
                return filesReaderFactory.Invoke(fileName);
            }
            catch (Exception)
            {
                FailApplication($"Can not read given file: {fileName}");
            }
            return Enumerable.Empty<string>();
        }

        private void UpdateFont(string font)
        {
            try
            {
                settings.TextSettings.Family = new FontFamily(font);
            }
            catch (Exception)
            {
                FailApplication("Font is incorrect");
            }
        }

        private void UpdateColor(string colorName)
        {
            settings.Palette.FontColor = ExtractColorFromName(colorName);
        }

        private void SaveImage(Image image, string destination)
        {
            try
            {
                imageSaver.SaveImage(image, destination);
            }
            catch (Exception)
            {
                FailApplication($"Can't save picture of cloud at given file path: {destination}");
            }
        }

        private Color ExtractColorFromName(string colorName)
        {
            var color = Color.FromName(colorName);
            if (!color.IsKnownColor)
                FailApplication($"Font color ({colorName}) is unknown");
            return color;
        }

        private void FailApplication(string message)
        {
            Console.WriteLine(message);
            System.Environment.Exit(1);
        }
    }
}