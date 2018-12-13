namespace TagsCloudContainer.TagFontSizeCalculator
{
    public class LinearTagFontSizeCalculator : ITagFontSizeCalculator
    {
        private ITagFontSizeCalculatorSettings Settings { get; }
        private int? minFontSize;
        private int MinFontSize => minFontSize ?? (int) (minFontSize = Settings.MinFontSize);
        private int? maxFontSize;
        private int MaxFontSize => maxFontSize ?? (int) (maxFontSize = Settings.MaxFontSize);

        public LinearTagFontSizeCalculator(ITagFontSizeCalculatorSettings settings)
        {
            Settings = settings;
        }

        public float Calculate(int count, int maxCount)
        {
            return (count / (float) maxCount) * (MaxFontSize - MinFontSize) + MinFontSize;
        }
    }
}