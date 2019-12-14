using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using TagCloudGenerator.CloudLayouters;
using TagCloudGenerator.TagClouds;
using TagCloudGenerator.Tags;

namespace TagCloudGenerator.Tests.WrongVisualization
{
    public class WrongVisualizationCloud : TagCloud<TagType>
    {
        private readonly Rectangle[] allRectangles;
        private readonly (Rectangle, Rectangle) wrongRectanglesPair;

        public WrongVisualizationCloud(Color backgroundColor,
                                       Dictionary<TagType, TagStyle> tagStyleByTagType,
                                       IEnumerable<Rectangle> allRectangles) : base(backgroundColor, tagStyleByTagType)
        {
            if (allRectangles is null)
                throw new ArgumentNullException(nameof(allRectangles));

            this.allRectangles = allRectangles.ToArray();

            if (this.allRectangles.Length == 0)
                throw new ArgumentException("Empty sequence was passed", nameof(allRectangles));
        }

        public WrongVisualizationCloud(Color backgroundColor,
                                       Dictionary<TagType, TagStyle> tagStyleByTagType,
                                       (Rectangle, Rectangle) wrongRectanglesPair,
                                       IEnumerable<Rectangle> allRectangles = null) : base(backgroundColor,
                                                                                           tagStyleByTagType)
        {
            if (wrongRectanglesPair.Item1.Size.IsEmpty && wrongRectanglesPair.Item2.Size.IsEmpty)
                throw new ArgumentException("Both of rectangles have empty size", nameof(wrongRectanglesPair));

            this.allRectangles = allRectangles?.ToArray() ?? new Rectangle[0];
            this.wrongRectanglesPair = wrongRectanglesPair;
        }

        protected override Action<Tag> GetTagDrawer(Graphics graphics) =>
            tag =>
            {
                using var pen = new Pen(tag.Style.TextColor) { Alignment = PenAlignment.Inset };
                graphics.DrawRectangle(pen, tag.TagBox);
            };

        protected override IEnumerable<Tag> GetTags(string[] cloudStrings,
                                                    Graphics graphics,
                                                    ICloudLayouter circularCloudLayouter) =>
            from rectangle in allRectangles
            let tagType = wrongRectanglesPair.Item1 == rectangle ? TagType.FirstWrong :
                          wrongRectanglesPair.Item2 == rectangle ? TagType.SecondWrong :
                                                                   TagType.Normal
            let tagStyle = TagStyleByTagType[tagType]
            select new Tag(null, tagStyle, rectangle);
    }
}