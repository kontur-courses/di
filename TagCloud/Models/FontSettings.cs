using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Models
{
    public class FontSettings
    {
        public readonly float defaultFontSize;
        public readonly FontFamily fontFamily;
        public readonly FontStyle fontStyle;
        public readonly Color color;

        public FontSettings()
        {
            this.defaultFontSize = 12;
            this.fontFamily = new FontFamily("Arial");
            this.fontStyle = FontStyle.Bold;
            this.color = Color.Black;
        }

        public FontSettings(string fontName)
        {
            this.defaultFontSize = 12;
            this.fontFamily = new FontFamily(fontName);
            this.fontStyle = FontStyle.Bold;
            this.color = Color.Black;
        }
    }
}
