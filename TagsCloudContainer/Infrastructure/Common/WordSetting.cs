using System.Drawing;

namespace TagsCloudContainer.Infrastructure.Common
{
    public class WordSetting
    {
        public string FontName { get; }
        
        public string Color { get; }
        
        public WordSetting(string fontName, string color)
        {
            FontName = fontName;
            Color = color;
        }
    }
}