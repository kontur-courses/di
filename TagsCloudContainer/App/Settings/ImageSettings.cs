namespace TagsCloudContainer.App.Settings
{
    public class ImageSettings
    {
        public static readonly ImageSettings Default = new ImageSettings(500, 500);

        private ImageSettings(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}