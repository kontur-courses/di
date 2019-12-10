using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.Models;

namespace TagCloud.IServices
{
    public class FontSettingsFactory : IFontSettingsFactory
    {
        public FontSettings CreateFontSettings(string fontName)
        {
            return new FontSettings(fontName);
        }
    }
}
