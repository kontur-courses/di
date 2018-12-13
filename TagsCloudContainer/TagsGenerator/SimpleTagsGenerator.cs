using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Tag;
using TagsCloudContainer.TagFontSizeCalculator;

namespace TagsCloudContainer.TagsGenerator
{
    public class SimpleTagsGenerator : ITagsGenerator
    {
        private ITagsGeneratorSettings Settings { get; }
        private ITagFontSizeCalculator FontSizeCalculator { get; }

        public SimpleTagsGenerator(ITagsGeneratorSettings settings, ITagFontSizeCalculator fontSizeCalculator)
        {
            Settings = settings;
            FontSizeCalculator = fontSizeCalculator;
        }

        public IEnumerable<ITag> GenerateTags(IDictionary<string, int> wordsFrequency)
        {
            var words = wordsFrequency.Keys;

            return words.Select(word => new SimpleTag(word, GetFont(wordsFrequency, word)));
        }

        private Font GetFont(IDictionary<string, int> wordsFrequency, string word)
        {
            var fontFamily = Settings.FontFamily;

            var maxCount = wordsFrequency.Values.Max();
            var fontSize = FontSizeCalculator.Calculate(wordsFrequency[word], maxCount);

            return new Font(fontFamily, fontSize);
        }
    }
}
