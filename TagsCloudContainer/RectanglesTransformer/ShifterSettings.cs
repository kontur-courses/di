using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.RectanglesTransformer
{
    public class ShifterSettings : IShifterSettings
    {
        public Size Offset { get; }

        public ShifterSettings(ILayouterSettings settings, Size imageSize)
        {
            var oldCenter = settings.Center;
            var optimalCenter = VisualizerСalculations.GetCenter(imageSize);
            var offset = new Size(optimalCenter) - new Size(oldCenter);
            Offset = offset;
        }
    }
}
