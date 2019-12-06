using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using TagCloudGenerator.CloudLayouters;
using TagCloudGenerator.Tags;

namespace TagCloudGenerator.TagClouds
{
    public class CommonWordsCloud : TagCloud
    {
        private const string MutualFont = "Bahnschrift SemiLight";

        private static readonly Dictionary<TagType, TagStyle> tagStyleByTagType = new Dictionary<TagType, TagStyle>
        {
            [TagType.Large] = new TagStyle(Color.Black, new Font(MutualFont, 30)),
            [TagType.Medium] = new TagStyle(Color.Black, new Font(MutualFont, 18))
        };

        protected override Color BackgroundColor => Color.White;

        protected override Action<Tag> GetTagDrawer(Graphics graphics)
        {
            var brushByColor = new Dictionary<Color, Brush>();

            return tag =>
            {
                using var pen = new Pen(Color.Black, 1) { Alignment = PenAlignment.Inset };

                graphics.FillRectangle(GetBrush(Color.Cornsilk, brushByColor), tag.TagBox);
                graphics.DrawRectangle(pen, tag.TagBox);
                graphics.DrawString(tag.Text, tag.Style.Font, GetBrush(tag.Style.TextColor, brushByColor),
                                    tag.TagBox, TagStyle.TextFormat);
            };
        }

        protected override IEnumerable<Tag> GetTags(string[] cloudStrings, Graphics graphics,
                                                    ICloudLayouter circularCloudLayouter)
        {
            for (var i = 0; i < cloudStrings.Length; i++)
            {
                var tagText = cloudStrings[i];
                var tagType = i == 0 ? TagType.Large : TagType.Medium;
                var tagStyle = tagStyleByTagType[tagType];

                var sizeF = graphics.MeasureString(tagText, tagStyle.Font);
                var size = new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));

                var tagBox = circularCloudLayouter.PutNextRectangle(size);

                yield return new Tag(tagText, tagStyle, tagBox);
            }
        }
    }
}