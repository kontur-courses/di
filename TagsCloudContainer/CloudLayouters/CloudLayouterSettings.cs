using System.Drawing;

namespace TagsCloudContainer.CloudLayouters
{
    public class CloudLayouterSettings
    {
        public ICloudLayoutingAlgorithm Algorithm { get; set; }
        public Point Center { get; set; }
        public int RectangleSquareMultiplier { get; set; }
    }
}