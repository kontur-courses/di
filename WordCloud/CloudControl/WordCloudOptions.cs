using WordCloud.WordCloudRenedering;

namespace WordCloud.CloudControl
{
    public class WordCloudOptions
    {
        public Vizualizer Vizualizer { get; set; }
        public LayoutTypes LayoutType { get; set; }

        public WordCloudOptions(LayoutTypes layoutType, Vizualizer vizualizer)
        {
            this.Vizualizer = vizualizer;
            this.LayoutType = layoutType;
        }
    }
}
