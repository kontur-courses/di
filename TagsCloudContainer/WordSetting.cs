using System.Drawing;
using System.IO;

namespace TagsCloudContainer
{
    public class WordSetting
    {
        public string FontName { get; }
        public SolidBrush Brush { get; }
        
        public WordSetting(string fontName, string color)
        {
            FontName = fontName;
            Brush = new SolidBrush(Color.FromName(color));
        }
    }
}