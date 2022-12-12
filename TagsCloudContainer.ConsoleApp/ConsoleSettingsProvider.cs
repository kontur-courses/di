using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.ConsoleApp
{
    internal class ConsoleSettingsProvider : ISettingsProvider
    {
        private readonly TextReaderSettings textReaderSettings;
        private readonly WordColorSettings wordColorSettings;
        private readonly WordFontSettings wordFontSettings;
        private readonly OutputImageSettings outputImageSettings;

        public ConsoleSettingsProvider(Options options)
        {
            textReaderSettings = ParseTextReaderSettings(options);
            wordColorSettings = ParseWordColorSettings(options);
            wordFontSettings = ParseWordFontSettings(options);
            outputImageSettings = ParseOutputImageSettings(options);
        }

        private static TextReaderSettings ParseTextReaderSettings(Options options)
        {
            return new TextReaderSettings() { Filename = options.InputWordFilename };
        }
        private static WordColorSettings ParseWordColorSettings(Options options)
        {
            var colorTypeConverter = TypeDescriptor.GetConverter(typeof(Color));
            return new WordColorSettings()
            {
                MinFrequencyColor = (Color)colorTypeConverter.ConvertFromString(options.MinFrequencyColorString)!,
                MaxFrequencyColor = (Color)colorTypeConverter.ConvertFromString(options.MaxFrequencyColorString)!
            };
        }
        private static WordFontSettings ParseWordFontSettings(Options options)
        {
            return new WordFontSettings()
            {
                FontFamily = options.FontFamily,
                FontSizeSettings = new WordFontSizeSettings()
                {
                    MinFrequencyFontSize = options.MinFrequncyFontSize,
                    MaxFrequencyFontSize = options.MaxFrequncyFontSize
                }
            };
        }
        private static OutputImageSettings ParseOutputImageSettings(Options options)
        {
            return new OutputImageSettings()
            {
                Filename = options.OutputImageFilename,
                Width = options.OutputImageWidth,
                Height = options.OutputImageWidth
            };
        }

        public TextReaderSettings GetTextReaderSettings() => textReaderSettings;
        public WordColorSettings GetWordColorSettings() => wordColorSettings;
        public WordFontSettings GetWordFontSettings() => wordFontSettings;
        public OutputImageSettings GetOutputImageSettings() => outputImageSettings;
    }
}