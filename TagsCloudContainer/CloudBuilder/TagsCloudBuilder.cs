using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.Tags;
using TagsCloudContainer.TextParsers;
using TagsCloudContainer.Themes;

namespace TagsCloudContainer.CloudBuilder
{
    public class TagsCloudBuilder : ICloudBuilder
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly ITheme theme;

        public TagsCloudBuilder(ICloudLayouter cloudLayouter, ITheme theme)
        {
            this.cloudLayouter = cloudLayouter;
            this.theme = theme;
        }


        public IEnumerable<Tag> BuildTagsCloud(List<MiniTag> miniTags)
        {
            var tags = CreateTagsFromMiniTags(miniTags);
            foreach (var tag in tags)
            {
                var size = TextRenderer.MeasureText(tag.Word, tag.Font);
                tag.Rectangle = cloudLayouter.PutNextRectangle(size);
                yield return tag;
            }
        }

        private IEnumerable<Tag> CreateTagsFromMiniTags(List<MiniTag> miniTags)
        {
            var tags = new List<Tag>();
            var firstRange = (int) (miniTags.Count * 0.5);
            var secondRange = miniTags.Count - firstRange - 1;

            tags.Add(new Tag(40, theme.FontFamily, miniTags.First().Word));

            tags.AddRange(miniTags
                .Skip(1)
                .Take(firstRange)
                .Select(miniTag => new Tag(30, theme.FontFamily, miniTag.Word)));

            tags.AddRange(miniTags
                .Skip(1 + firstRange)
                .Take(secondRange)
                .Select(miniTag => new Tag(20, theme.FontFamily, miniTag.Word)));

            return tags;
        }
    }
}