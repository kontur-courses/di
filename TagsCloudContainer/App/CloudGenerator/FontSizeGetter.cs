using TagsCloudContainer.Infrastructure.CloudGenerator;

namespace TagsCloudContainer.App.CloudGenerator
{
    internal class FontSizeGetter : IFontSizeGetter
    {
        private readonly int defaultFontSize = 10;

        public double GetFontSize(string word, double frequency)
        {
            return defaultFontSize * (1 + frequency * defaultFontSize);
        }
    }
}