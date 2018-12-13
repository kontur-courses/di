using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Configuration;
using TagsCloudContainer.Tag;
using TagsCloudContainer.TagFontSizeCalculator;

namespace TagsCloudContainer.TagsGenerator
{
    public class SimpleTagsGenerator : ITagsGenerator
    {
        private IConfiguration Configuration { get; }
        private ITagFontSizeCalculator FontSizeCalculator { get; }

        public SimpleTagsGenerator(IConfiguration configuration, ITagFontSizeCalculator fontSizeCalculator)
        {
            Configuration = configuration;
            FontSizeCalculator = fontSizeCalculator;
        }

        public IEnumerable<ITag> GenerateTags(IDictionary<string, int> wordsFrequency)
        {
            var words = wordsFrequency.Keys;

            return words.Select(word => new SimpleTag(word, GetFont(wordsFrequency, word)));
        }

        private Font GetFont(IDictionary<string, int> wordsFrequency, string word)
        {
            var fontFamily = Configuration.FontFamily;

            var maxCount = wordsFrequency.Values.Max();
            var fontSize = FontSizeCalculator.Calculate(wordsFrequency[word], maxCount);

            return new Font(fontFamily, fontSize);
        }
    }
}
