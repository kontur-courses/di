namespace CloodLayouter.Infrastructer
{
    public class ImageSettings
    {
        public ImageSettings(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
        public int Width { get;  } 
        public int Height { get; } 
    }
}