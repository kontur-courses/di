using System.Drawing;
using TagsCloudVisualization.PointGenerators;

namespace TagsCloudVisualization
{
    public class CloudParameters
    {
        public Size ImageSize { get; set; }
        public Color Color { get; set; }
        public string FontName { get; set; }
        public IPointGenerator PointGenerator { get; set; }
    }
}