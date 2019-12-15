using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloudGenerator.GeneratorCore.CloudLayouters;
using TagCloudGenerator.GeneratorCore.Tags;

namespace TagCloudGenerator.GeneratorCore.TagClouds
{
    // instantiated implicitly by ITagCloudOptions.ConstructCloud
    // ReSharper disable once ClassNeverInstantiated.Global
    public class WebCloud : TagCloud<TagType>
    {
        private const int LargeTagsFrequency = 8;
        private const int MediumTagsFrequency = 3;

        public WebCloud(Color backgroundColor, Dictionary<TagType, TagStyle> tagStyleByTagType) :
            base(backgroundColor, tagStyleByTagType) { }

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
                var tagStyle = TagStyleByTagType[tagType];

                var sizeF = graphics.MeasureString(tagText, tagStyle.Font);
                var size = new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));

                var tagBox = circularCloudLayouter.PutNextRectangle(size);

                yield return new Tag(tagText, tagStyle, tagBox);
            }

            static TagType GetTagType(int tagIndex)
            {
                if (tagIndex == 0)
                    return TagType.Central;
                if (tagIndex % LargeTagsFrequency == 0)
                    return TagType.Large;

                return tagIndex % MediumTagsFrequency == 0 ? TagType.Medium : TagType.Small;
            }
        }
    }
}