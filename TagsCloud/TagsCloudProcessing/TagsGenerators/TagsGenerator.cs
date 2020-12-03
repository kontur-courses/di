using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.Factory;
using TagsCloud.Layouter;
using TagsCloud.TextProcessing;
using TagsCloud.TextProcessing.WordsConfig;

namespace TagsCloud.TagsCloudProcessing.TegsGenerators
{
    public class TagsGenerator : ITagsGenerator
    {
        private readonly IServiceFactory<IRectanglesLayouter> layouterFactory;
        private readonly WordConfig wordsConfig;

        public TagsGenerator(IServiceFactory<IRectanglesLayouter> layouterFactory, WordConfig wordsConfig)
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
                var size = TextRenderer.MeasureText(word.Word, currentFont);
                tags.Add(new Tag(word.Word, layouter.PutNextRectangle(size), currentFont));
                count--;
            }

            return tags;
        }
    }
}
