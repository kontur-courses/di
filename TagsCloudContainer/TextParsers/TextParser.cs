using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.TextParsers
{
    public class TextParser : ITextParser
    {
        private readonly TextSettings textSettings;

        public TextParser(TextSettings textSettings)
        {
            this.textSettings = textSettings;
        }


        public List<WordFrequency> Parse(string text)
        {
            var words = text.Split(Environment.NewLine.ToCharArray());
            var convertedWords = new List<string>();
            foreach (var word in words)
            {
                var convertedWord = word;
                if (textSettings.WordFilters.Any(filter => !filter.Validate(word)))
                    continue;
                foreach (var converter in textSettings.WordConverters) convertedWord = converter.Convert(word);
                convertedWords.Add(convertedWord);
            }

            return convertedWords
                .GroupBy(word => word)
                .Select(group => new WordFrequency(group.Key, group.Count()))
                .OrderByDescending(miniTag => miniTag.Frequency)
                .ThenBy(miniTag => miniTag.Word)
                .Take(textSettings.CountWords)
                .ToList();
        }
    }
}