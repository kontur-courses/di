using System.Drawing;

namespace TagsCloudContainer.Settings
{
    public class DefaultCloudSettings : ICloudSettings
    {
        public int WordsToDisplay { get; set; } = 100;
        public Point CenterPoint => new Point(Size.Width / 2, Size.Height / 2);
        public Size Size { get; set; } = new Size(1500, 1500);
    }
}