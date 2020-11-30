using System.Drawing;
using TagCloud.Settings;
using TagCloud.TagClouds;

namespace TagCloud.TagClouds
{
    public class CircleTagCloud : RectangleTagCloud
    {
        private readonly CloudSettings settings;

        public CircleTagCloud(CloudSettings settings)
        {
            this.settings = settings;
        }

        public Point Center => settings.Center;
        public double StartRadius => settings.StartRadius;
    }
}
