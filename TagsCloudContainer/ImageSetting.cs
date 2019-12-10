using System.Drawing;

namespace TagsCloudContainer
{
    public class ImageSetting
    {
        public int Height { get; }
        public int Width { get; }
        
        public Color BackGround { get; }

        public ImageSetting(int height, int width, string color)
        {
            Height = height;
            Width = width;
            BackGround = Color.FromName(color);
        }
    }
}