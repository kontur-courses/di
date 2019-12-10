using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircularCloudLayouter;
using TagsCloudForm.CircularCloudLayouter;
using TagsCloudForm.Common;

namespace TagsCloudForm.Actions
{
    public class CloudPainterFactory
    {
        private readonly IImageHolder imageHolder;
        private readonly IPalette palette;
        private readonly Func<Point, ICircularCloudLayouter> circularCloudLayouterFactory;
        public CloudPainterFactory(IImageHolder imageHolder,
            IPalette palette,
            Func<Point, ICircularCloudLayouter> circularCloudLayouterFactory)
        {
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.circularCloudLayouterFactory = circularCloudLayouterFactory;
        }

        public CloudPainter Create(CircularCloudLayouterSettings settings)
        {
            var layouter = circularCloudLayouterFactory.Invoke(new Point(settings.CenterX, settings.CenterY));
            layouter.SetCompression(settings.XCompression, settings.YCompression);
            return new CloudPainter(imageHolder, settings, palette, layouter);
        }
    }
}
