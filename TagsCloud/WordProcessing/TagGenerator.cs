using System.Collections.Generic;
using TagsCloud.Interfaces;
using System.Linq;
using System;

namespace TagsCloud.WordProcessing
{
    public class TagGenerator : ITagGenerator
    {
        private readonly IFontSettingsGenerator fontGenerator;
        private readonly IColorScheme colorGenerator;

        public TagGenerator(IFontSettingsGenerator fontGenerator, IColorScheme colorGenerator)
        {
            this.fontGenerator = fontGenerator;
            this.colorGenerator = colorGenerator;
        }

        public IEnumerable<Tag> GenerateTag(IEnumerable<(string word, int frequncy)> wordStatistics)
        {
            var currentPosition = 0;
            var countWord = wordStatistics.Count();
            var result = new List<Tag>();
            foreach (var wordStatistic in wordStatistics.OrderByDescending(pair => pair.frequncy))
            {
                var fontSettings = fontGenerator.GetFontSizeForCurrentWord(wordStatistic, currentPosition, countWord);
                var color = colorGenerator.GetColorForCurrentWord(wordStatistic, currentPosition, countWord);
                currentPosition++;
                result.Add(new Tag(fontSettings, color, wordStatistic.word));
            }
            return result;
        }
    }
}
