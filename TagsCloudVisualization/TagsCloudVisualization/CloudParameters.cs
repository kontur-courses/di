using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.PointGenerators;

namespace TagsCloudVisualization
{
    public class CloudParameters
    {
        public Size ImageSize { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public string FontName { get; set; }
        public IPointGenerator PointGenerator { get; set; }
        public ImageFormat OutFormat { get; set; }
    }
}