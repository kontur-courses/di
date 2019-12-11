using System.Drawing;

namespace TagsCloudVisualization.Logic.Painter
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