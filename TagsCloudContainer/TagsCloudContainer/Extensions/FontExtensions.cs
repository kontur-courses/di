using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.Extensions
{
    public static class FontExtensions
    {
        public static Font GetAdjustedFont(this Font source, Graphics g, string graphicString, 
            int containerWidth, int maxFontSize, int minFontSize)
        {
            Font testFont = null;
            for (int adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize--)
            {
                testFont = new Font(source.Name, adjustedSize, source.Style);

                SizeF adjustedSizeNew = g.MeasureString(graphicString, testFont);

                if (containerWidth > Convert.ToInt32(adjustedSizeNew.Width))
                {
                    return testFont;
                }
            }

            return source;
        }
    }
}
