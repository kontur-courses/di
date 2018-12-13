using System.Drawing;
using TagsCloudVisualization.InterfacesForSettings;

namespace TagsCloudVisualization.TagsCloud
{
    public class ImageSettings : IImageSettings
    {
        public Size ImageSize { get; set; }
        public Point Center { get; set; }
        public Font Font { get; set; }

        public ImageSettings()
        {
            ImageSize = new Size(2000, 2000);
            Center = new Point(1000, 1000);
            Font = new Font("Times New Roman", 45, FontStyle.Regular, GraphicsUnit.Point);
        }
    }
}