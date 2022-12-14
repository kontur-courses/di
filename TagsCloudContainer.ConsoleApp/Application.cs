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
            Result result;
            if (!(result = wordReader.TryReadWords(settingsProvider.GetTextReaderSettings().Filename, out var words)).Success)
            {
                outStream.Write(result.Message);
                return;
            }

            var wordFrequencies = GetWordFrequencies(wordPreparer.Prepare(words));
            
            var wordFontSettings = settingsProvider.GetWordFontSettings();
            wordFontSettings.FontSizeSettings.WordFrequencies = wordFrequencies;

            var wordColorSettings = settingsProvider.GetWordColorSettings();
            wordColorSettings.WordFrequencies = wordFrequencies;

            words = wordFrequencies.Keys.ToArray();
            var outputImageSettings = settingsProvider.GetOutputImageSettings();
            var pictureSize = new Size(outputImageSettings.Width, outputImageSettings.Height);

            var wordPlates = tagsCloudGenerator.GeneratePlates(words, new PointF(pictureSize.Width / 2.0F, pictureSize.Height / 2.0F), wordFontSettings);
            wordPlateVisualizer.DrawPlatesAndSave(wordPlates, pictureSize, settingsProvider.GetOutputImageSettings().Filename, wordColorSettings);

            outStream.Write($"Generated and saved to '{ settingsProvider.GetOutputImageSettings().Filename }'");
        }

        private static Dictionary<string, int> GetWordFrequencies(IEnumerable<string> words)
        {
            var frequencies = new Dictionary<string, int>();
            foreach(var word in words)
            {
                if (!frequencies.ContainsKey(word))
                    frequencies.Add(word, 0);
                frequencies[word]++;
            }
            return frequencies;
        }
    }
}