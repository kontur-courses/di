using System.Drawing;
using WordCloud.TextAnalyze.Words;

namespace WordCloud.CloudControl
{
    public class CloudItem
    {
        public CloudItem(Rectangle rectangle, IWord word, Font font)
        {
            Rectangle = rectangle;
            Word = word;
            Font = font;
        }
        public Rectangle Rectangle { get; set; }
        public IWord Word { get; set; }
        public Font Font { get; set; }
    }
}
