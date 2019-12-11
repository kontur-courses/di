using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.RectanglesTransformer;

namespace TagsCloudContainer.ImageSizeCalculator
{
    public class DefaultImageSizeCalculator: IImageSizeCalculator
    {
        public Size CalculateImageSize(ICollection<Rectangle> rectangles)
        {
            return TransformerСalculations.GetOptimalSizeForImage(rectangles, 5);
        }
    }
}
