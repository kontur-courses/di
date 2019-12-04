using System.Drawing;
using System.IO;

namespace TagsCloudContainer
{
    public class WordSetting
    {
        public Font Font { get; }
        public SolidBrush Brush { get; }
        
        public WordSetting(string fontName, int size, string color)
        {
            Font = new Font(fontName, size);
            Brush = new SolidBrush(Color.FromName(color));
        }
    }
}