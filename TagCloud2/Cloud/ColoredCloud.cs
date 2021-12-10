using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2
{
    public class ColoredCloud : IColoredCloud
    {
        Dictionary<Rectangle, Color> colorDict = new();
        public IColoredCloud GetFromCloudLayouter(ICloudLayouter cloud, IColoringAlgorithm coloringAlgorithm)
        {
            var rectangles = cloud.GetRectangles();
            foreach (var rect in rectangles)
            {
                colorDict.Add(rect, coloringAlgorithm.GetColor(rect));
            }
            return this;
        }

        Dictionary<Rectangle, Color> IColoredCloud.GetColoredRectangles()
        {
            return colorDict;
        }
    }
}
