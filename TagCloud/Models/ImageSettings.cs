using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Models
{
    public class ImageSettings
    {
        public int Width { get; }
        public int Height { get; }
        public string FontName { get; }
        public string PaletteName { get; }
        public ImageSettings(int width, int height, string fontName, string paletteName)
        {
            PaletteName = paletteName;
            Width = width;
            Height = height;
            FontName = fontName;
        }
    }
}
