using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.SourcesTypes;

namespace TagsCloudVisualization.Providers.Layouter
{
    public class CloudInfo
    {
        public readonly IEnumerable<DrawableWord> DrawableSource;

        public CloudInfo(IEnumerable<DrawableWord> drawableWordSource)
        {
            //TODO toarray
            DrawableSource = drawableWordSource.ToArray();
        }

        public Point TranslateTransform => new Point(LeftDownPointOfCloud.X, LeftDownPointOfCloud.Y);

        public Size SizeOfCloud =>
            new Size(RightUpperPointOfCloud.X - LeftDownPointOfCloud.X,
                RightUpperPointOfCloud.Y - LeftDownPointOfCloud.Y);

        private Point LeftDownPointOfCloud
        {
            get
            {
                if (!DrawableSource.Any())
                    return new Point(0, 0);

                var minX = DrawableSource.Select(r => r.Place.X).Min();
                var minY = DrawableSource.Select(r => r.Place.Y).Min();
                return new Point(minX, minY);
            }
        }

        private Point RightUpperPointOfCloud
        {
            get
            {
                if (!DrawableSource.Any())
                    return new Point(0, 0);

                var maxX = DrawableSource.Select(r => r.Place.X + r.Place.Width).Max();
                var maxY = DrawableSource.Select(r => r.Place.Y + r.Place.Height).Max();
                return new Point(maxX, maxY);
            }
        }
    }
}