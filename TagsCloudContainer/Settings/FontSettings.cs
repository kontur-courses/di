using System.Drawing;

namespace TagsCloudContainer.Settings
{
    public class FontSettings
    {
        public Font Font { get; set; } = new Font(new FontFamily("Calibri Light"), 1);
        public float FontSizeFactor { get; set; } = 10;
    }
}