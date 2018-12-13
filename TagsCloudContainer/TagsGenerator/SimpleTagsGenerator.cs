using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Tag;
using TagsCloudContainer.TagFontSizeCalculator;

namespace TagsCloudContainer.TagsGenerator
{
    public class SimpleTagsGenerator : ITagsGenerator
    {
        private readonly ITagsGeneratorSettings settings;
        private readonly ITagFontSizeCalculator fontSizeCalculator;

        public SimpleTagsGenerator(ITagsGeneratorSettings settings, ITagFontSizeCalculator fontSizeCalculator)
        {
            this.settings = settings;
            this.fontSizeCalculator = fontSizeCalculator;
        }

        public IEnumerable<ITag> GenerateTags(IDictionary<string, int> wordsFrequency)
        {
            var words = wordsFrequency.Keys;

            return words.Select(word => new SimpleTag(word, GetFont(wordsFrequency, word)));
        }

        private Font GetFont(IDictionary<string, int> wordsFrequency, string word)
        {
            var fontFamily = settings.FontFamily;

            var maxCount = wordsFrequency.Values.Max();
            var fontSize = fontSizeCalculator.Calculate(wordsFrequency[word], maxCount);

            return new Font(fontFamily, fontSize);
        }
    }
}
