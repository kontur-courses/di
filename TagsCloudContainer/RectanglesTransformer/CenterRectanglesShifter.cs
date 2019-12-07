using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.RectanglesTransformer
{
    public class CenterRectanglesShifter : IRectanglesTransformer
    {
        private readonly IShifterSettings settings;

        public CenterRectanglesShifter(IShifterSettings settings)
        {
            this.settings = settings;
        }

        public IList<Rectangle> TransformRectangles(IEnumerable<Rectangle> rectangles)
        {
            return VisualizerСalculations.GetRectanglesWithOptimalLocation(rectangles, settings.Offset);
        }
    }
}
