using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Common.Settings;

namespace TagsCloudVisualization.Common.Tags
{
    public class StyledTagBuilder : IStyledTagBuilder
    {
        private readonly ITagStyleSettings tagStyleSettings;
        private int colorIndex;
        private int fontIndex;

        public StyledTagBuilder(ITagStyleSettings tagStyleSettings)
        {
            this.tagStyleSettings = tagStyleSettings;
            colorIndex = -1;
            fontIndex = -1;
        }

        public IEnumerable<StyledTag> GetStyledTags(IEnumerable<Tag> tags)
        {
            return tags.Select(tag => new StyledTag(tag, GetNewTagStyle(tag)));
        }

        private TagStyle GetNewTagStyle(Tag tag)
        {
            return new TagStyle()
            {
                ForegroundColor = GetTagColor(),
                Font = GetTagFont(tag.Weight)
            };
        }

        private Color GetTagColor()
        {
            colorIndex = (colorIndex + 1) % tagStyleSettings.ForegroundColors.Length;
            return tagStyleSettings.ForegroundColors[colorIndex];
        }

        private Font GetTagFont(float weight)
        {
            fontIndex = (fontIndex + 1) % tagStyleSettings.FontFamilies.Length;
            return new Font(tagStyleSettings.FontFamilies[fontIndex],
                tagStyleSettings.Size - tagStyleSettings.SizeScatter + tagStyleSettings.SizeScatter * 2 * weight * 10);
        }
    }
}