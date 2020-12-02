namespace TagsCloud.ClientGUI.Infrastructure
{
    public class ImageSettings
    {
        private const int MinWidth = 300;
        private const int MaxWidth = 1920;
        private const int MinHeight = 300;
        private const int MaxHeight = 1080;
        public int Width { get; set; }
        public int Height { get; set; }

        public void NormalizeSize()
        {
            if (Width > MaxWidth || Width < MinWidth)
                Width = Width > MaxWidth ? MaxWidth : MinWidth;
            if (Height > MaxHeight || Height < MinHeight)
                Height = Height > MaxHeight ? MaxHeight : MinHeight;
        }
    }
}