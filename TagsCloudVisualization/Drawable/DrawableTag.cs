using System.Drawing;

namespace TagsCloudVisualization.Drawable
{
    internal class DrawableTag : IDrawable
    {
        private readonly Color color;
        private readonly Font font;
        private readonly Tag tag;

        public DrawableTag(Tag tag, Rectangle bounds, Font font, Color color)
        {
            this.tag = tag;
            this.font = font;
            this.color = color;
            Bounds = bounds;
        }

        public Rectangle Bounds { get; }

        public void Draw(Graphics graphics)
        {
            using var brush = new SolidBrush(color);
            graphics.DrawString(tag.Word, font, brush, Bounds);
        }
    }
}