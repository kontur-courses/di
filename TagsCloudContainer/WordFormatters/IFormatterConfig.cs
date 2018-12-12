using System.Drawing;

namespace TagsCloudContainer.WordFormatters
{
    public interface IFormatterConfig
    {
        Font Font { get; }

        Color Color { get; }

        bool FrequentWordsAsHuge { get; }

        float FontMultiplier { get; }
    }
}