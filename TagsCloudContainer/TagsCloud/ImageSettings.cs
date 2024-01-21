using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TagsCloud
{
    public class ImageSettings : IImageSettings
    {
        public Color BackgroundColor { get; } = Color.White;
        public Color FontColor { get; } = Color.Black;
        public int ImageWidth { get; } = 1600;
        public int ImageHeight { get; } = 1200;

        public Font GetFont()
        {
            return new Font("Verdana", 20);
        }

    }
}
