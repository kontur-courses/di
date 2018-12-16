using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommandLine;
using TagsCloudContainer.ImageCreators;
using TagsCloudContainer.ImageSavers;
using TagsCloudContainer.ProjectSettings;
using TagsCloudContainer.Readers;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordsHandlers;

namespace TagsCloudContainer.Clients
{
    public class ConsoleClient : IClient
    {
        private readonly IImageCreator imageCreator;
        private readonly IWordsHandler wordsHandler;
        private readonly IImageSaver imageSaver;
        private readonly IReader filesReader;
        private readonly ISettingsManager settings;

        public ConsoleClient(IWordsHandler wordsHandler, IImageCreator imageCreator, IReader filesReader,
            ISettingsManager settings, IImageSaver imageSaver)
        {
            this.wordsHandler = wordsHandler;
            this.imageCreator = imageCreator;
            this.imageSaver = imageSaver;
            this.filesReader = filesReader;
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
            UpdateFontFamily(consoleClientOptions.FontFamily);
            UpdateColor(consoleClientOptions.Color);
            UpdateFontSize(consoleClientOptions.BaseFontSize);
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
                return filesReader.Read(fileName);
            }
            catch (Exception exception)
            {
                FailApplication($"Can not read given file: {fileName}. {exception.Message}. ");
            }
            return Enumerable.Empty<string>();
        }

        private void UpdateFontFamily(string font)
        {
            try
            {
                settings.TextSettings.Family = new FontFamily(font);
            }
            catch (Exception)
            {
                FailApplication("FontFamily is incorrect");
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

        private void UpdateFontSize(int font)
        {
            if (font < 10 || font > 100)
                FailApplication("Base font size should be in range from 10 to 100");
            settings.ImageSettings.BaseFontSize = font;
        }

        private Color ExtractColorFromName(string colorName)
        {
            var color = Color.FromName(colorName);
            if (!color.IsKnownColor)
                FailApplication($"FontFamily color ({colorName}) is unknown");
            return color;
        }

        private void FailApplication(string message)
        {
            Console.WriteLine(message);
            System.Environment.Exit(1);
        }
    }
}