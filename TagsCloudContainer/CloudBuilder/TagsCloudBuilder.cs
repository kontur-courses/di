using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.Tags;
using TagsCloudContainer.TextParsers;

namespace TagsCloudContainer.CloudBuilder
{
    public class TagsCloudBuilder : ICloudBuilder
    {
        public TagsCloudBuilder(ICloudLayouter cloudLayouter, ITextParser textParser)
        {
            this.textParser = textParser;
            this.cloudLayouter = cloudLayouter;
            tags = CreateTagsFromTuple(textParser.Parse());
        }

        private ICloudLayouter cloudLayouter { get; }
        private ITextParser textParser { get; }
        private IEnumerable<Tag> tags { get; }

        public IEnumerable<Tag> BuildTagsCloud()
        {
            foreach (var tag in tags)
            {
                var size = TextRenderer.MeasureText(tag.Word, tag.Font);
                tag.Rectangle = cloudLayouter.PutNextRectangle(size);
                yield return tag;
            }
        }

        private IEnumerable<Tag> CreateTagsFromTuple(List<(string, int)> words)
        {
            var listTags = new List<Tag>();
            var firstRange = (int) (words.Count * 0.5);
            var secondRange = words.Count - firstRange - 1;

            listTags.Add(new Tag(40, "Arial", words.First().Item1));

            listTags.AddRange(words
                .Skip(1)
                .Take(firstRange)
                .Select(pair => new Tag(30, "Arial", pair.Item1)));
            
            listTags.AddRange(words
                .Skip(1 + firstRange)
                .Take(secondRange)
                .Select(pair => new Tag(20, "Arial", pair.Item1)));

            return listTags;
        }
    }
}