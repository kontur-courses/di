using System.Collections.Generic;
using System.Drawing;
using TagCloudGenerator.Tags;
using TagType = TagCloudGenerator.Tests.WrongVisualization.TagType;

namespace TagCloudGenerator.Tests
{
    public static class TestsHelper
    {
        public static Color BackgroundColor => Color.White;

        public static Dictionary<TagType, TagStyle> TagStyleByTagType => new Dictionary<TagType, TagStyle>
        {
            [TagType.Normal] = new TagStyle(Color.LightGray, null),
            [TagType.SecondWrong] = new TagStyle(Color.Crimson, null),
            [TagType.FirstWrong] = new TagStyle(Color.Teal, null)
        };

        public static (Rectangle, Rectangle)? GetAnyPairOfIntersectingRectangles(Rectangle[] rectangles)
        {
            for (int i = 0; i < rectangles.Length; i++)
                for (int j = i + 1; j < rectangles.Length; j++)
                    if (rectangles[i].IntersectsWith(rectangles[j]))
                        return (rectangles[i], rectangles[j]);
            return null;
        }
    }
}