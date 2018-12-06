using System.Drawing;
using System.Linq;
using TagsCloudVisualization.TagsCloud.CircularCloud;

namespace TagsCloudVisualization.TagsCloud.CloudConstruction
{
    public class RectangleGenerator
    {
        public CircularCloudLayouter Cloud { get; set; }
        public PointGenerator PointGenerator { get; set; }

        public RectangleGenerator(CircularCloudLayouter cloud)
        {
            Cloud = cloud;
            PointGenerator = new PointGenerator(cloud);
        }

        public Rectangle GetNextRectangle(Size size)
        {
            var location = PointGenerator.GetNextPointArchimedesSpiral(size);
            var rectangle = new Rectangle(location, size);
            while (!CheckLocation(rectangle))
            {
                location = PointGenerator.GetNextPointArchimedesSpiral(size);
                rectangle = new Rectangle(location, size);
            }
            return rectangle;
        }
        private bool CheckLocation(Rectangle rec)
        {
            if (rec == new Rectangle())
                return false;
            return Cloud.Rectangles.All(rec1 => rec1.Y >= rec.Y + rec.Height || rec.Y >= rec1.Y + rec1.Height ||
                                          rec.X >= rec1.X + rec1.Width || rec1.X >= rec.X + rec.Width);
        }
    }
}