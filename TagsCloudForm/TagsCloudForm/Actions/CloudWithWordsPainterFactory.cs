using System;
using System.Collections.Generic;
using System.Drawing;
using CircularCloudLayouter;
using TagsCloudForm.CircularCloudLayouter;
using TagsCloudForm.Common;

namespace TagsCloudForm.Actions
{
    public class CloudWithWordsPainterFactory
    {
        private readonly IImageHolder imageHolder;
        private readonly IPalette palette;
        private readonly Func<Point, ICircularCloudLayouter> circularCloudLayouterFactory;
        public CloudWithWordsPainterFactory(IImageHolder imageHolder,
            IPalette palette, 
            Func<Point, ICircularCloudLayouter> circularCloudLayouterFactory)
        {
            this.imageHolder = imageHolder;
            this.palette = palette;
            this.circularCloudLayouterFactory = circularCloudLayouterFactory;
        }

        public CloudWithWordsPainter Create(CircularCloudLayouterWithWordsSettings settings, Dictionary<string, int> words)
        {
            var layouter = circularCloudLayouterFactory.Invoke(new Point(settings.CenterX, settings.CenterY));
            return new CloudWithWordsPainter(imageHolder, settings, palette, layouter, words);
        }
    }
}
