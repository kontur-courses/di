using WordCloud.CloudControl;
using WordCloudImageGenerator.LayoutCraetion.Layouters;

namespace WordCloudImageGenerator
{
    public class WordCloudConfig
    {
        public Vizualizer Vizualizer { get; private set; }
        public LayoutTypes LayoutType { get; private set; }
        public int MinFontSize { get; set; }
        public int MaxFontSize { get; set; }

        public WordCloudConfig(LayoutTypes layoutType, Vizualizer vizualizer, int maxFontSize, int minFontSize)
        {
            this.Vizualizer = vizualizer;
            this.LayoutType = layoutType;
            this.MaxFontSize = maxFontSize;
            this.MinFontSize = minFontSize;
        }
    }
}
