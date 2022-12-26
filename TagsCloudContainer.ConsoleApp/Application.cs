using FluentResults;
using java.lang;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.Settings;
using TagsCloudContainer.Infrastructure.WordPreparers;
using TagsCloudContainer.Infrastructure.WordReaders;

namespace TagsCloudContainer.ConsoleApp
{
    internal class Application
    {
        private readonly ISettingsProvider settingsProvider;
        private readonly IWordReader wordReader;
        private readonly IWordPreparer wordPreparer;
        private readonly ITagsCloudGenerator tagsCloudGenerator;
        private readonly WordPlateVisualizer wordPlateVisualizer;

        public Application(ISettingsProvider settingsProvider,
                           IWordReader wordReader,
                           IWordPreparer wordPreparer,
                           ITagsCloudGenerator tagsCloudGenerator,
                           WordPlateVisualizer wordPlateVisualizer)
        {
            this.settingsProvider = settingsProvider;
            this.wordReader = wordReader;
            this.wordPreparer = wordPreparer;
            this.tagsCloudGenerator = tagsCloudGenerator;
            this.wordPlateVisualizer = wordPlateVisualizer;
        }

        public void Run(TextWriter outStream)
        {
            var result = wordReader.TryReadWords(settingsProvider.GetTextReaderSettings().Filename)
                                   .Bind(wds => wordPreparer.Prepare(wds))
                                   .Bind(GeneratePlates)
                                   .Bind(info => (Result)wordPlateVisualizer.DrawPlatesAndSave(info.Value.Plates, info.Value.PictureSize, settingsProvider.GetOutputImageSettings().Filename, info.Value.WordColorSettings))
                                   .OnSuccess(r => outStream.Write($"Generated and saved to '{settingsProvider.GetOutputImageSettings().Filename}'"))
                                   .OnFail(r => HandleFailedResult(r, outStream));
        }

        private Result<dynamic> GeneratePlates(string[] wds)
        {
            var wordFrequencies = GetWordFrequencies(wds);

            var wordFontSettings = settingsProvider.GetWordFontSettings();
            wordFontSettings.FontSizeSettings.WordFrequencies = wordFrequencies;

            var wordColorSettings = settingsProvider.GetWordColorSettings();
            wordColorSettings.WordFrequencies = wordFrequencies;

            var words = wordFrequencies.Keys.ToArray();
            var outputImageSettings = settingsProvider.GetOutputImageSettings();
            var pictureSize = new Size(outputImageSettings.Width, outputImageSettings.Height);

            var generatePlatesResult = tagsCloudGenerator.GeneratePlates(words,
                                                                         new PointF(pictureSize.Width / 2.0F, pictureSize.Height / 2.0F),
                                                                         wordFontSettings);
            return generatePlatesResult.ToResult(r => new { Plates = r, PictureSize = pictureSize, WordColorSettings = wordColorSettings });
        }

        private static void HandleFailedResult(Result result, TextWriter outStream)
        {
            outStream.Write(result);
        }

        private static Dictionary<string, int> GetWordFrequencies(IEnumerable<string> words)
        {
            var frequencies = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencies.ContainsKey(word))
                    frequencies.Add(word, 0);
                frequencies[word]++;
            }
            return frequencies;
        }
    }
}