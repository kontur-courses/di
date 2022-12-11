using TagCloudGraphicalUserInterface.Interfaces;

namespace TagCloudGraphicalUserInterface.Settings
{
    public class ImageSettings : IImage
    {
        public int Width { get; set; } = 500;
        public int Height { get; set; } = 500;
    }
}
