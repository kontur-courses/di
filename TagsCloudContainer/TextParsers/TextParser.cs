using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordHandler;

namespace TagsCloudContainer.TextParsers
{
    public class TextParser : ITextParser
    {
        public TextParser(IFileReader fileReader, IWordHandler wordHandler, TextSettings textSettings)
        {
            this.fileReader = fileReader;
            this.wordHandler = wordHandler;
            countWords = textSettings.CountWords;
        }

        private int countWords { get; }
        private IFileReader fileReader { get; }
        private IWordHandler wordHandler { get; }

        public List<(string, int)> Parse()
        {
            var text = fileReader.Read();
            return Regex.Split(text.ToLower(), @"\W+")
                .Select(e => wordHandler.Transform(e))
                .Where(word => !string.IsNullOrWhiteSpace(word) && !IsBoring(word))
                .GroupBy(word => word)
                .Select(group => (group.Key, group.Count()))
                .OrderByDescending(tuple => tuple.Item2)
                .ThenBy(tuple => tuple.Item1)
                .Take(countWords)
                .ToList();
        }

        private bool IsBoring(string word)
        {
            return word.Length < 2;
        }
    }
}