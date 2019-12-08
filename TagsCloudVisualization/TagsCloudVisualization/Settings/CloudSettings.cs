using System.Drawing;


namespace TagsCloudVisualization.Settings
{
    public class CloudSettings
    {
        public int Radius { get; private set; }
        public Point Center { get; private set; }

        public CloudSettings(Point center, int radius)
        {
            Radius = radius;
            Center = center;
        }
    }
}
