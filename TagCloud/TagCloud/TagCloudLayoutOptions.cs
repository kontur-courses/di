namespace TagsCloudVisualization
{
    public class TagCloudLayoutOptions
    {
        public TagCloudLayoutOptions(ISpiralGenerator spiral, Point center, double sizeCoefficient)
        {
            Spiral = spiral;
            Center = center;
            SizeCoefficient = sizeCoefficient;
        }

        public ISpiralGenerator Spiral { get; private set; }
        public Point Center { get; private set; }
        public double SizeCoefficient { get; private set; }
    }
}
