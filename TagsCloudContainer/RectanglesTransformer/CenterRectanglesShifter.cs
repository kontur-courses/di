using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.RectanglesTransformer
{
    public class CenterRectanglesShifter : IRectanglesTransformer
    {
        private readonly ILayouterSettings settings;

        public CenterRectanglesShifter(ILayouterSettings settings)
        {
            this.settings = settings;
        }

        public IList<Rectangle> TransformRectangles(IEnumerable<Rectangle> rectangles, Size imageSize)
        {
            var optimalCenter = TransformerСalculations.GetCenter(imageSize);
            var offset = new Size(optimalCenter) - new Size(settings.Center);
            return TransformerСalculations.GetRectanglesWithOptimalLocation(rectangles, offset);
        }
    }
}
