using System.Drawing;
using TagCloud.WordColoring;

namespace TagCloud.ImageProcessing
{
    public class ImageSettings : IImageSettings
    {
        public string WordColoringAlgorithmName { get; set; } = "Black";

        public Size Size { get; set; } = new Size(1000,1000);

        public Color BackgroundColor { get; set; } = Color.White;

        public FontFamily FontFamily { get; set; } = new FontFamily("Times New Roman");

        public int MinFontSize { get; set; } = 12;

        public int MaxFontSize { get; set; } = 36;

        
    }
}
