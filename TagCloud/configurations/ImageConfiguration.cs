using System.Drawing;

namespace TagCloud.configurations
{
    public class ImageConfiguration : IImageConfiguration
    {
        private readonly Color backgroundColor;
        private readonly int width;
        private readonly int height;

        public ImageConfiguration(Color backgroundColor, int width, int height)
        {
            this.backgroundColor = backgroundColor;
            this.width = width;
            this.height = height;
        }

        public Color GetBackgroundColor() => backgroundColor;

        public int GetWidth() => width;

        public int GetHeight() => height;
    }
}