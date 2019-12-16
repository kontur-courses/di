using System.Drawing;

namespace TagsCloudContainer.Infrastructure.Common
{
    public class ImageSetting
    {
        public int Height { get; }
        public int Width { get; }
        public string Format { get; }
        public string Name { get;  }
        
        public Color BackGround { get; }

        public ImageSetting(int height, int width, string color, string format, string name)
        {
            Height = height;
            Width = width;
            BackGround = Color.FromName(color);
            Format = format;
            Name = name;
        }
    }
}