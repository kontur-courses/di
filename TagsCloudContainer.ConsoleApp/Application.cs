using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.Settings;

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

        public void Run() 
        {
            Result result;
            if (!(result = wordReader.TryReadWords(settingsProvider.GetTextReaderSettings().Filename, out var words)).Success)
            {
                Console.WriteLine(result.Message);
                return;
            }

            var wordFrequencies = GetWordFrequencies(wordPreparer.Prepare(words));
            
            var wordFontSizeSettings = settingsProvider.GetWordFontSizeSettings();
            wordFontSizeSettings.WordFrequencies = wordFrequencies;

            var wordColorSettings = settingsProvider.GetWordColorSettings();
            wordColorSettings.WordFrequencies = wordFrequencies;

            words = wordFrequencies.Keys.ToArray();
            var pictureSize = new Size(500, 500);
            var wordPlates = tagsCloudGenerator.GeneratePlates(words, "Consolas", new PointF(pictureSize.Width / 2.0F, pictureSize.Height / 2.0F), wordFontSizeSettings);
            wordPlateVisualizer.DrawPlatesAndSave(wordPlates, pictureSize, settingsProvider.GetSaveTagsCloudSettings().Filename, wordColorSettings);

            Console.WriteLine("Generated and saved");
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