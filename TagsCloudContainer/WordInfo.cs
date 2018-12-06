using System.Drawing;

namespace TagsCloudContainer
{
    public class WordInfo
    {
        public string Word { get; set; }
        public Rectangle Rect { get; set; }
        public float FontSize { get; set; }
        public int Frequency { get; set; }
    }
}