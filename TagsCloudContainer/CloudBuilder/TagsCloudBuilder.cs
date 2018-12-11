using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.Settings;
using TagsCloudContainer.Tags;
using TagsCloudContainer.TextParsers;

namespace TagsCloudContainer.CloudBuilder
{
    public class TagsCloudBuilder : ICloudBuilder
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly ImageSettings imageSettings;

        public TagsCloudBuilder(ICloudLayouter cloudLayouter, ImageSettings imageSettings)
        {
            this.cloudLayouter = cloudLayouter;
            this.imageSettings = imageSettings;
        }


        public IEnumerable<Tag> BuildTagsCloud(List<WordFrequency> miniTags)
        {
            var tags = CreateTagsFromMiniTags(miniTags);
            foreach (var tag in tags)
            {
                var size = TextRenderer.MeasureText(tag.Word, tag.Font);
                tag.Rectangle = cloudLayouter.PutNextRectangle(size);
                yield return tag;
            }
        }

        private IEnumerable<Tag> CreateTagsFromMiniTags(List<WordFrequency> miniTags)
        {
            var tags = new List<Tag>();
            var firstRange = (int) (miniTags.Count * 0.5);
            var secondRange = miniTags.Count - firstRange - 1;

            tags.Add(new Tag(40, imageSettings.Theme.FontFamily, miniTags.First().Word));

            tags.AddRange(miniTags
                .Skip(1)
                .Take(firstRange)
                .Select(miniTag => new Tag(30, imageSettings.Theme.FontFamily, miniTag.Word)));

            tags.AddRange(miniTags
                .Skip(1 + firstRange)
                .Take(secondRange)
                .Select(miniTag => new Tag(20, imageSettings.Theme.FontFamily, miniTag.Word)));

            return tags;
        }
    }
}