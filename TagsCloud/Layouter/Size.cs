namespace TagsCloudVisualization
{
    public class Size
    {
        public double Width { get; }
        public double Height { get; }
        public double Square => Width * Height;

        public Size(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }
}