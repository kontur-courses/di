using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class SingleColorTagPainter : ITagPainter
    {
        private readonly Color paintColor;

        public SingleColorTagPainter(Color paintColor)
        {
            this.paintColor = paintColor;
        }

        public Color GetTagColor() => paintColor;
    }
}