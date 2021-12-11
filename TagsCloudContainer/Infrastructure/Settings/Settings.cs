using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Infrastructure
{
    public class Settings
    {
        public Palette Palette { get; set; }
        public Font Font { get; set; }
        public Size ImageSize { get; set; }
        public IPreprocessor[] Preprocessors { get; set; }
        public IFilter[] Filters { get; set; }
        public ImageFormat Format { get; set; }
        public Point Center
        {
            get
            {
                var center = ImageSize / 2;
                return new Point(center.Height, center.Width);
            }
        }
    }
}
