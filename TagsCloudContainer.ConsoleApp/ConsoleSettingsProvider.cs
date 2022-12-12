using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
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
        private readonly WordFontSettings wordFontSizeSettings;
        private readonly OutputImageSettings outputImageSettings;

        public ConsoleSettingsProvider(string[] args)
        {
            var parserResult = Parser.Default.ParseArguments<Options>(args);

            if (parserResult.Errors.Any())
            {
                var sentenceBuilder = SentenceBuilder.Create();
                throw new ArgumentException("Can't parse arguments:\n" + string.Join("\n", parserResult.Errors.Select(error => sentenceBuilder.FormatError(error))));
            }

            var options = parserResult.Value;
            textReaderSettings = new TextReaderSettings() { Filename = options.InputWordFilename };
            outputImageSettings = new OutputImageSettings() { Filename = options.OutputImageFilename, Width = options.OutputImageWidth, Height = options.OutputImageWidth };
            wordColorSettings = new WordColorSettings() { MinFrequencyColor = Color.Black, MaxFrequencyColor = Color.Blue };
            wordFontSizeSettings = new WordFontSettings() 
            {
                FontFamily = options.FontFamily,
                FontSizeSettings = new WordFontSizeSettings()
                {
                    MinFrequencyFontSize = options.MinFrequncyFontSize,
                    MaxFrequencyFontSize = options.MaxFrequncyFontSize
                }
            };
        }

        public OutputImageSettings GetOutputImageSettings() => outputImageSettings;

        public TextReaderSettings GetTextReaderSettings() => textReaderSettings;

        public WordColorSettings GetWordColorSettings() => wordColorSettings;

        public WordFontSettings GetWordFontSettings() => wordFontSizeSettings;
    }
}