using System.Drawing;
using WordCloudImageGenerator.Parsing.Word;

namespace WordCloudImageGenerator
{
    public class CloudItem
    {
        public CloudItem(Rectangle rectangle, IWord word, Font font)
        {
            Rectangle = rectangle;
            Word = word;
            Font = font;
        }
        public Rectangle Rectangle { get; }
        public IWord Word { get; }
        public Font Font { get; }
    }
}
