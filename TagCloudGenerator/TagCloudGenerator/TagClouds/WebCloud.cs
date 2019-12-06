using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloudGenerator.CloudLayouters;
using TagCloudGenerator.Tags;

namespace TagCloudGenerator.TagClouds
{
    public class WebCloud : TagCloud
    {
        private const string MutualFont = "Bahnschrift SemiLight";
        private const int LargeTagsFrequency = 8;
        private const int MediumTagsFrequency = 3;

        private static readonly Dictionary<TagType, TagStyle> tagStyleByTagType = new Dictionary<TagType, TagStyle>
        {
            [TagType.Central] = new TagStyle(Color.White, new Font(MutualFont, 60)),
            [TagType.Large] = new TagStyle(Color.FromArgb(255, 102, 0), new Font(MutualFont, 22)),
            [TagType.Medium] = new TagStyle(Color.FromArgb(212, 85, 0), new Font(MutualFont, 18)),
            [TagType.Small] = new TagStyle(Color.FromArgb(160, 90, 44), new Font(MutualFont, 13))
        };

        protected override Color BackgroundColor => Color.FromArgb(0, 34, 43);

        protected override Action<Tag> GetTagDrawer(Graphics graphics)
        {
            // here i'm caching some disposable objects. If you are gonna use a lot of colors, think about creating your
            // own disposable class with this dictionary and dispose him instance after using.
            var brushByColor = new Dictionary<Color, Brush>();

            return tag => graphics.DrawString(
                tag.Text, tag.Style.Font, GetBrush(tag.Style.TextColor, brushByColor), tag.TagBox, TagStyle.TextFormat);
        }

        protected override IEnumerable<Tag> GetTags(
            string[] cloudStrings, Graphics graphics, ICloudLayouter circularCloudLayouter)
        {
            for (var i = 0; i < cloudStrings.Length; i++)
            {
                var tagText = cloudStrings[i];
                var tagType = GetTagType(i);
                var tagStyle = tagStyleByTagType[tagType];

                var sizeF = graphics.MeasureString(tagText, tagStyle.Font);
                var size = new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));

                var tagBox = circularCloudLayouter.PutNextRectangle(size);

                yield return new Tag(tagText, tagStyle, tagBox);
            }

            static TagType GetTagType(int tagIndex)
            {
                if (tagIndex == 0) return TagType.Central;
                if (tagIndex % LargeTagsFrequency == 0) return TagType.Large;

                return tagIndex % MediumTagsFrequency == 0 ? TagType.Medium : TagType.Small;
            }
        }
    }
}