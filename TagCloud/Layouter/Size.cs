namespace TagCloud.Layouter
{
    public class Size
    {
        public Size(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Width { get; }
        public double Height { get; }
        public double Square => Width * Height;
    }
}