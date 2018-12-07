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
        private readonly IWordHandler wordHandler;
        private readonly TextSettings textSettings;
        
        public TextParser(IWordHandler wordHandler, TextSettings textSettings)
        {
            this.wordHandler = wordHandler;
            this.textSettings = textSettings;
        }


        public List<MiniTag> Parse(string text)
        {
            return text.Split('\n')
                .Select(e => wordHandler.Transform(e))
                .Where(word => !string.IsNullOrWhiteSpace(word) && !IsBoring(word))
                .GroupBy(word => word)
                .Select(group => new MiniTag(group.Key, group.Count()))
                .OrderByDescending(miniTag => miniTag.Count)
                .ThenBy(miniTag => miniTag.Word)
                .Take(textSettings.CountWords)
                .ToList();
        }

        private bool IsBoring(string word)
        {
            return word.Length < 2;
        }
    }
}