using System.Drawing;
using TagCloud.WordColoring;

namespace TagCloud.ImageProcessing
{
    public interface IImageSettings
    {
        Size Size { get; }
        Color BackgroundColor { get; }
        FontFamily FontFamily { get; }
        int MaxFontSize { get; }
        int MinFontSize { get; }
        string WordColoringAlgorithmName { get; }
    }
}
