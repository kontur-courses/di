using System.Drawing;
using TagsCloudDrawer;
using TagsCloudDrawer.ColorGenerators;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.Drawable.Rectangles
{
    public class RectangleDrawable : IDrawable
    {
        private readonly IColorGenerator _colorGenerator;
        public Rectangle Bounds { get; }

        public RectangleDrawable(Rectangle bounds, IColorGenerator colorGenerator)
        {
            _colorGenerator = colorGenerator;
            Bounds = bounds;
        }

        public void Draw(Graphics graphics)
        {
            using var brush = new SolidBrush(_colorGenerator.Generate());
            graphics.FillRectangle(brush, Bounds);
        }

        public IDrawable Shift(Size vector) => new RectangleDrawable(Bounds.Shift(vector), _colorGenerator);
    }
}