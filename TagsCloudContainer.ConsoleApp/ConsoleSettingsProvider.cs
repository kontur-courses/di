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
        private readonly SaveTagsCloudSettings saveTagsCloudSettings;
        private readonly TextReaderSettings textReaderSettings;
        private readonly WordColorSettings wordColorSettings;
        private readonly WordFontSizeSettings wordFontSizeSettings;

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
            saveTagsCloudSettings = new SaveTagsCloudSettings() { Filename = options.OutputTagsCloudFilename };
            wordColorSettings = new WordColorSettings() { MinFrequencyColor = Color.Black, MaxFrequencyColor = Color.Blue };
            wordFontSizeSettings = new WordFontSizeSettings() { MinFrequencyFontSize = 5F, MaxFrequencyFontSize = 18F };
        }

        public SaveTagsCloudSettings GetSaveTagsCloudSettings() => saveTagsCloudSettings;

        public TextReaderSettings GetTextReaderSettings() => textReaderSettings;

        public WordColorSettings GetWordColorSettings() => wordColorSettings;

        public WordFontSizeSettings GetWordFontSizeSettings() => wordFontSizeSettings;
    }
}