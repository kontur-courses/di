namespace TagsCloudContainer.App.Settings
{
    public class ImageSettings
    {
        public ImageSettings()
        {
            SetDefault();
        }

        private void SetDefault()
        {
            Width = 500;
            Height = 500;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}