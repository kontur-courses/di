using System.Drawing;

namespace TagsCloudForm
{
    public class GraphicDrawer : IGraphicDrawer
    {
        private readonly Graphics graphics;
        public GraphicDrawer(Image image)
        {
            this.graphics = Graphics.FromImage(image);
        }
        public void DrawRectangle(Pen rectPen, Rectangle rectangle)
        {
            graphics.DrawRectangle(rectPen, rectangle);
        }

        public void FillRectangle(Brush brush, Rectangle rectangle)
        {
            graphics.FillRectangle(brush, rectangle);
        }

        public void FillRectangle(Brush brush, int x, int y, int width, int height)
        {
            graphics.FillRectangle(brush, x, y, width, height);
        }

        public void Dispose()
        {
            graphics.Dispose();
        }

        public void DrawString(string word, Font font, Brush textBrush, PointF point)
        {
            graphics.DrawString(word, font, textBrush, point);
        }
    }
}
