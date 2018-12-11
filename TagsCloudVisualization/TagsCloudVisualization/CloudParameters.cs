using System;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.PointGenerators;

namespace TagsCloudVisualization
{
    public class CloudParameters
    {
        public Size ImageSize { get; set; }
        public Func<float, Color> ColorFunc { get; set; }
        public string FontName { get; set; }
        public IPointGenerator PointGenerator { get; set; }
        public ImageFormat OutFormat { get; set; }
    }
}