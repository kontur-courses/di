using System;

namespace TagsCloudVisualization.Styling.TagColorizer
{
    public class RandomTagColorizer : ITagColorizer
    {
        public string GetTagColor(string[] tagColors, Tag tag)
        {
            var random = new Random();
            return tagColors[random.Next(0, tagColors.Length)];
        }
    }
}