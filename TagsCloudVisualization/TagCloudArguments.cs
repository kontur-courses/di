using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class TagCloudArguments
    {
        public Func<Size> Factory { get; }
        public int Count { get; }
        public Color Color { get; }

        public TagCloudArguments(int count, Func<Size> factory, Color color)
        {
            Factory = factory;
            Count = count;
            Color = color;
        }
    }
}