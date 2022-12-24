using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.GUI
{
    internal class GUISettingsProvider : ISettingsProvider
    {
        private readonly Options options;

        public GUISettingsProvider(Options options)
        {
            this.options = options;
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
                    MinFrequencyFontSize = options.MinFrequencyFontSize,
                    MaxFrequencyFontSize = options.MaxFrequencyFontSize
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

        public TextReaderSettings GetTextReaderSettings() => ParseTextReaderSettings(options);
        public WordColorSettings GetWordColorSettings() => ParseWordColorSettings(options);
        public WordFontSettings GetWordFontSettings() => ParseWordFontSettings(options);
        public OutputImageSettings GetOutputImageSettings() => ParseOutputImageSettings(options);
    }
}