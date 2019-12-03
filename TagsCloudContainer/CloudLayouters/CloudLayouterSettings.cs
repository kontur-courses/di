using System.Drawing;

namespace TagsCloudContainer.CloudLayouters
{
    public class CloudLayouterSettings
    {
        public ICloudLayoutingAlgorithm Algorithm { get; set; }
        public int RectangleSquareMultiplier { get; set; } = 100;
        public double RectangleStep { get; set; } = 0.1;
        public int RectangleBroadness { get; set; } = 1;
    }
}