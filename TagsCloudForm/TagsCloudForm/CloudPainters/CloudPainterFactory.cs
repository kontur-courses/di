using System;
using System.Drawing;
using CircularCloudLayouter;
using TagsCloudForm.Actions;
using TagsCloudForm.Common;

namespace TagsCloudForm.CloudPainters
{
    public class CloudPainterFactory : IPainterFactory
    {
        private readonly IImageHolder imageHolder;
        private readonly IPalette palette;
        private readonly Func<Point, ICircularCloudLayouter> circularCloudLayouterFactory;
        private readonly CircularCloudLayouterSettings.ICircularCloudLayouterSettings settings;
        public CloudPainterFactory(IImageHolder imageHolder,
            IPalette palette,
            CircularCloudLayouterSettings.ICircularCloudLayouterSettings settings,
            Func<Point, ICircularCloudLayouter> circularCloudLayouterFactory)
        {
            
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.circularCloudLayouterFactory = circularCloudLayouterFactory;
            this.settings = settings;
        }

        public ICloudPainter Create()
        {
            var layouter = circularCloudLayouterFactory.Invoke(new Point(settings.CenterX, settings.CenterY));
            layouter.SetCompression(settings.XCompression, settings.YCompression);
            return new CloudPainter(imageHolder, settings, palette, layouter);
        }
    }
}
