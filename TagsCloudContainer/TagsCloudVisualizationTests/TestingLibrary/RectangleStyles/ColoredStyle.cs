using System.Drawing;
using TagsCloudVisualizationTests.Interfaces;

namespace TagsCloudVisualizationTests.TestingLibrary.RectangleStyles
{
    public class ColoredStyle : IRectangleStyle
    {
        private readonly Color[] colors =
        {
            Color.Red,
            Color.Blue,
            Color.Green
        };

        private int index;

        public void Draw(Graphics graphics, Rectangle rectangle) =>
            graphics.DrawRectangle(new Pen(GetNextColor(), 1), rectangle);

        public Color GetNextColor() =>
            colors[index++ % colors.Length];
    }
}