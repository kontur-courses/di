using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.Layouter;
using TagsCloud.TextProcessing;

namespace TagsCloud.TagsCloudProcessing.TegsGenerators
{
    public class TagsGenerator : ITagsGenerator
    {
        public IEnumerable<Tag> CreateTags(IEnumerable<WordInfo> words, ILayouter layouter, Font font)
        {
            var sortedWords = words.OrderByDescending(info => info.Frequence).ToList();
            var tags = new List<Tag>();

            var count = 40;
            foreach (var word in sortedWords.Take(30))
            {
                var currentFont = new Font(font.FontFamily, count);
                var size = TextRenderer.MeasureText(word.Value, currentFont);
                tags.Add(new Tag(word.Value, layouter.PutNextRectangle(size), currentFont));
                count--;
            }

            return tags;
        }

        public string GetGeneratorName() => "30 самых частотных";
    }
}
