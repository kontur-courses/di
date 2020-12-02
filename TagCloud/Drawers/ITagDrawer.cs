using System.Drawing;

namespace TagCloud.Drawers
{
    public interface ITagDrawer
    {
        public Size MeasureStringSize(string text, Font font);

        public Bitmap DrawString(string text, Font font, Point textPosition, Color? color = null);
    }
}