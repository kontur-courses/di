using TagsCloudVisualization;

namespace TagCloud
{
    public class TagCloudOptions
    {
        public TagCloudOptions(ISpiralGenerator spiral, Point center, double sizeCoefficient)
        {
            this.LayoutOptions = new TagCloudLayoutOptions(spiral, center, sizeCoefficient);
        }

        public TagCloudLayoutOptions LayoutOptions { get; }
        public ISpiralGenerator Spiral { get; private set; }
        public Point Center { get; private set; }
        public double SizeCoefficient => 3;
    }
}
