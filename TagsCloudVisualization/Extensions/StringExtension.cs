using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public static class StringExtension
    {
        public static Size GetSurroundingRectangleSize(this string text, Font font) => 
            TextRenderer.MeasureText(text, font);
    }
}
