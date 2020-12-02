using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.Layouter.Factory;
using TagsCloud.TextProcessing;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloud.TagsCloudProcessing.TegsGenerators
{
    public class TagsGenerator : ITagsGenerator
    {
        private readonly IRectanglesLayoutersFactory layouterFactory;
        private readonly IWordsConfig wordsConfig;

        public TagsGenerator(IRectanglesLayoutersFactory layouterFactory, IWordsConfig wordsConfig)
        {
            this.layouterFactory = layouterFactory;
            this.wordsConfig = wordsConfig;
        }

        public IEnumerable<Tag> CreateTags(IEnumerable<WordInfo> words)
        {
            var layouter = layouterFactory.Create();
            var sortedWords = words.OrderByDescending(info => info.Frequence).ToList();
            var tags = new List<Tag>();
            var font = wordsConfig.FontName;

            var count = 30;
            foreach (var word in sortedWords.Take(30))
            {
                var currentFont = new Font(font.FontFamily, count);
                var size = TextRenderer.MeasureText(word.Value, currentFont);
                tags.Add(new Tag(word.Value, layouter.PutNextRectangle(size), currentFont));
                count--;
            }

            return tags;
        }
    }
}
