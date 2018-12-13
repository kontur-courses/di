namespace TagsCloudContainer.TagFontSizeCalculator
{
    public class LinearTagFontSizeCalculator : ITagFontSizeCalculator
    {
        private readonly ITagFontSizeCalculatorSettings settings;
        private int? minFontSize;
        private int MinFontSize => minFontSize ?? (int) (minFontSize = settings.MinFontSize);
        private int? maxFontSize;
        private int MaxFontSize => maxFontSize ?? (int) (maxFontSize = settings.MaxFontSize);

        public LinearTagFontSizeCalculator(ITagFontSizeCalculatorSettings settings)
        {
            this.settings = settings;
        }

        public float Calculate(int count, int maxCount)
        {
            return (count / (float) maxCount) * (MaxFontSize - MinFontSize) + MinFontSize;
        }
    }
}