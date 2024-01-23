using System.Drawing;
using TagsCloudVisualization.PointDistributors;

namespace TagsCloudVisualization
{
    public class Program
    {
        public const int ImageWidth = 1000;
        public const int ImageHeight = 1000;

        public static void Main(string[] args)
        {
            new CircularCloudLayouter(new Point(ImageWidth / 2, ImageHeight / 2), new Spiral());
        }
    }
}
