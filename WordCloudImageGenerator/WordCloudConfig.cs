using WordCloud.CloudControl;
using WordCloudImageGenerator.LayoutCraetion.Layouters;

namespace WordCloudImageGenerator
{
    public class WordCloudConfig
    {
        public LayoutTypes LayoutType { get; set; }
        public int MinFontSize { get; set; }
        public int MaxFontSize { get; set; }

        public WordCloudConfig(LayoutTypes layoutType, int maxFontSize = 20, int minFontSize = 8)
        {
            this.LayoutType = layoutType;
            this.MaxFontSize = maxFontSize;
            this.MinFontSize = minFontSize;
        }
    }
}
