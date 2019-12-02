using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudForm.Actions
{
    public class CloudPainterFactory
    {
        public CloudPainter Create(IImageHolder imageHolder,
            CircularCloudLayouterSettings settings, Palette palette, ICircularCloudLayouter layouter)
        {
            return new CloudPainter(imageHolder, settings, palette, layouter);
        }
    }
}
