using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class RandomTagPainter : ITagPainter
    {
        private readonly Random random;

        public RandomTagPainter()
        {
            random = new Random();
        }

        public void SetColorsForTagCollection(IEnumerable<Tag> tagCollection)
        {
            foreach (var tag in tagCollection)
            {
                tag.Color = GetRandomColor();
            }
        }

        private Color GetRandomColor()
        {
            return Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        }
    }
}