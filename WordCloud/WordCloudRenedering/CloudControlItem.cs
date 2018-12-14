using System.Drawing;
using WordCloud.TextAnalyze.Words;

namespace WordCloud.WordCloudRenedering
{
    public class CloudControlItem
    {
        public CloudControlItem(Rectangle rectangle, IWord word)
        {
            Rectangle = rectangle;
            Word = word;
        }
        public Rectangle Rectangle { get; set; }
        public IWord Word { get; set; }
    }
}
