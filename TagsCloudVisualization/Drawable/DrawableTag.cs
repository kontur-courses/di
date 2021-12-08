using System.Drawing;

namespace TagsCloudVisualization.Drawable
{
    public class DrawableTag : IDrawable
    {
        private readonly Tag tag;
        private readonly Font font;
        private readonly Color color;
        public Rectangle Bounds { get; }

        public DrawableTag(Tag tag, Rectangle bounds, Font font, Color color)
        {
            this.tag = tag;
            this.font = font;
            this.color = color;
            Bounds = bounds;
        }
        
        public void Draw(Graphics graphics)
        {
            using var brush = new SolidBrush(color);
            graphics.DrawString(tag.Word, font, brush, Bounds);
        }
    }
}