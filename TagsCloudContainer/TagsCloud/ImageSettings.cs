using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TagsCloud
{
    public class ImageSettings : IImageSettings
    {
        public Color BackgroundColor { get; init; } = Color.White;
        public Color FontColor { get; init; } = Color.Black;
        public int Width { get; set; } = 1600;
        public int Height { get; set; } = 1200;

        public Font GetFont()
        {
            return new Font("Verdana", 20);
        }

        public void UpdateImageSettings(int width, int height)
        {
            Width = width;
            Height = height;
        }

    }
}
