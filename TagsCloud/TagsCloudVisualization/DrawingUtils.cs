using System.Drawing;

namespace TagsCloudVisualization
{
    public static class DrawingUtils
    {
        public static SolidBrush GetBrushFromHexColor(string hexColor)
        {
            return new SolidBrush(ColorTranslator.FromHtml(hexColor));
        }
        
        public static Pen GetPenFromHexColor(string hexColor)
        {
            return new Pen(ColorTranslator.FromHtml(hexColor));
        }
    }
}