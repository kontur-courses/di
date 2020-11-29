using System.Drawing;

namespace TagCloud.Coloring
{
    public interface IPainter
    {
        void DrawAndFillRectangle(Rectangle rectangle, Graphics graphics);

        void DrawString(Rectangle rectangle, string str, string fontFamily, Graphics graphics);
    }
}