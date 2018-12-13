using TagsCloudContainer.Configuration;

namespace TagsCloudContainer.TagFontSizeCalculator
{
    public class LinearTagFontSizeCalculator : ITagFontSizeCalculator
    {
        private IConfiguration Configuration { get; }
        private int? minFontSize;
        private int MinFontSize => minFontSize ?? (int) (minFontSize = Configuration.MinFontSize);
        private int? maxFontSize;
        private int MaxFontSize => maxFontSize ?? (int) (maxFontSize = Configuration.MaxFontSize);

        public LinearTagFontSizeCalculator(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public float Calculate(int count, int maxCount)
        {
            return (count / (float) maxCount) * (MaxFontSize - MinFontSize) + MinFontSize;
        }
    }
}