using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class TagCloudBuilder
    {
        private readonly WordPreprocessor wordPreprocessor;
        private readonly ILayouter layouter;
        private readonly IWordColorPicker wordColorPicker;
        private readonly IWordFontPicker wordFontPicker;
        private readonly Dictionary<string, IFileWordsProvider> wordProviderByExtension;

        public TagCloudBuilder(
            ILayouter layouter,
            IEnumerable<IFileWordsProvider> wordProviders,
            IWordColorPicker wordColorPicker,
            IWordFontPicker wordFontPicker,
            WordPreprocessor wordPreprocessor)
        {
            this.layouter = layouter;
            this.wordColorPicker = wordColorPicker;
            this.wordFontPicker = wordFontPicker;
            this.wordPreprocessor = wordPreprocessor;

            wordProviderByExtension = wordProviders
                .SelectMany(wp => wp.AcceptedExtensions.Select(ext => (ext, wp)))
                .ToDictionary();
        }

        public IEnumerable<WordLayout> Build(string filePath, Color wordColor, Color bgColor, float fontSize)
        {
            wordColorPicker.SetBackgroundColor(bgColor);
            wordColorPicker.SetBaseWordColor(wordColor);
            wordFontPicker.SetBaseSize(fontSize);
            
            var words = GetWords(filePath);
            var filteredWords = wordPreprocessor.PreprocessWords(words);
            var wordCount = CountWords(filteredWords);
            var wordColors = wordColorPicker.PickColors(wordCount);
            var wordFonts = wordFontPicker.PickFonts(wordCount);

            foreach (var tuple in wordCount.OrderByDescending(t => t.count))
            {
                var word = tuple.word;
                var font = wordFonts[word];
                var color = wordColors[word];
                var rectangle = layouter.PutNextRectangle(font.GetStringSize(word).ToSize());

                yield return new WordLayout(word, rectangle, font, color);
            }
        }

        private static List<(string word, int count)> CountWords(IEnumerable<string> words)
        {
            var wordCount = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (!wordCount.ContainsKey(word))
                    wordCount[word] = 0;
                wordCount[word]++;
            }

            return wordCount.Select(pair => (pair.Key, pair.Value)).ToList();
        }

        private string[] GetWords(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            if (extension == null)
            {
                throw new ArgumentNullException(nameof(extension));
            }

            if (!wordProviderByExtension.TryGetValue(extension, out var wordsProvider))
            {
                throw new ArgumentException($"Unsupported extension: \"{extension}\"");
            }

            using (var stream = File.OpenRead(filePath))
            {
                return wordsProvider.GetWords(stream);
            }
        }
    }
}
