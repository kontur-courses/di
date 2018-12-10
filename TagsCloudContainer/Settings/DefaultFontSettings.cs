using System.Drawing;

namespace TagsCloudContainer.Settings
{
    public class DefaultFontSettings : IFontSettings
    {
        public FontFamily FontFamily { get; set; } = new FontFamily("Times New Roman");
        public Brush Brush { get; set; } = Brushes.Black;
    }
}