namespace TagsCloudContainer
{
    public class ImageSetting
    {
        public int Height { get; }
        public int Width { get; }

        public ImageSetting(int height, int width)
        {
            Height = height;
            Width = width;
        }
    }
}