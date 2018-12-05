namespace TagsCloudVisualization
{
    public class TagCloudLayoutOptions
    {
        public TagCloudLayoutOptions(ISpiralGenerator spiral, Point center, double sizeCoefficient)
        {
            this.Spiral = spiral;
            this.Center = center;
            this.SizeCoefficient = sizeCoefficient;
        }

        public ISpiralGenerator Spiral { get; private set; }
        public Point Center { get; private set; }
        public double SizeCoefficient { get; private set; }
    }
}
