using TagCloudGUI.Interfaces;

namespace TagCloudGUI.Settings
{
    public class ImageSettings : IImage
    {
        public int Width { get; set; } = 800;
        public int Height { get; set; } = 600;
    }
}
