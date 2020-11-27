using System.Drawing;
using System.Linq;
using TagsCloud.Core;

namespace TagsCloud.Visualization
{
    public class Program
    {
        public const int ImageWidth = 1920;
        public const int ImageHeight = 1080;
        private const int RectanglesCount = 5000;

        private static void Main()
        {
            var cloud = new CircularCloudLayouter(new Point(ImageWidth / 2, ImageHeight / 2));

            var rectangles = new SizeGenerator(5, 15, 5, 15)
                .GenerateSize(RectanglesCount)
                .Select(cloud.PutNextRectangle);

            var image = CircularCloudVisualization.CreateImage(rectangles, ImageWidth, ImageHeight);
            image.Save($"../../Images/{RectanglesCount}rectangles.jpg");
        }
    }
}
