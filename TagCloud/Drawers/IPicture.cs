using System.Drawing;

namespace TagCloud.Drawers
{
    public interface IPicture
    {
        public Size MeasureStringSize(string text, Font font);

        public void FillRectangle(Rectangle rectangle, Color? color = null);

        public void DrawCircle(Point position, float radius, Color? color = null);

        public void DrawString(string text, Font font, Point textPosition, Color? color = null);
        
        public void Save(string path = null, string outputFileName = "output");
    }
}