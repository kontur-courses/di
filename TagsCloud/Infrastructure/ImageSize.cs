namespace TagsCloud.Infrastructure
{
    public class ImageSize
    {
        private int width, height;
        private int maxWidth = 1920, maxHeight = 1080;
        public int Width
        {
            get => width;
            set
            {
                if (value > 0)
                {
                    width = value <= maxWidth ? value : maxWidth;
                }
            }
        }
        public int Height
        {
            get => height;
            set
            {
                if (value > 0)
                {
                    height = value <= maxHeight ? value : maxHeight;
                }
            }
        }

        public ImageSize() {}

        public ImageSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static ImageSize operator *(ImageSize size, double a)
        {
            return new ImageSize((int)(size.Width * a), (int)(size.height * a));
        }
    }
}
