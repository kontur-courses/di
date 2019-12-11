using System;
using System.Drawing;

namespace TagsCloudVisualization.Logic.Painter
{
    public class RandomTagPainter : ITagPainter
    {
        private readonly Random random;

        public RandomTagPainter()
        {
            random = new Random();
        }

        public Color GetTagColor()
        {
            return Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        }
    }
}