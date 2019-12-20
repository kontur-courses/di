using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Providers.Layouter.Interfaces;
using TagsCloudVisualization.Providers.Layouter.Spirals;
using TagsCloudVisualization.Results;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.SourcesTypes;

namespace TagsCloudVisualization.Providers.Layouter
{
    public class DrawableCloudLayouter : IDrawableProvider
    {
        private readonly SpiralFactory factory;
        private Point center;
        public List<Rectangle> Rectangles;
        private ISpiral spiralPointer;

        public DrawableCloudLayouter(SpiralFactory factory)
        {
            this.factory = factory;
        }

        public Result<List<DrawableWord>> GetDrawableSource(List<SizableWord> sizableSource,
            LayouterSettings settings)
        {
            var drawableSource = new List<DrawableWord>();
            Rectangles = new List<Rectangle>();
            center = settings.Center;
            spiralPointer = factory.Create(settings);

            foreach (var sizable in sizableSource)
            {
                var nextRect = PutNextRectangle(sizable.DrawSize);
                if (!nextRect.IsSuccess)
                    return Result.Fail<List<DrawableWord>>(nextRect.Error);
                drawableSource.Add(new DrawableWord(sizable.Value, nextRect.Value));
            }

            return drawableSource.AsResult();
        }

        private Result<Rectangle> PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.IsEmpty || rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                return Result.Fail<Rectangle>("Rectangle does not exist");

            var rect = new Rectangle(spiralPointer.GetSpiralCurrent(), rectangleSize);
            while (Rectangles.Any(currentR => currentR.IntersectsWith(rect)))
            {
                var currentPoint = spiralPointer.GetSpiralNext();
                rect.X = currentPoint.X;
                rect.Y = currentPoint.Y;
            }

            rect = GetRectangleMovedToCenter(rect);

            Rectangles.Add(rect);
            return rect;
        }

        private Rectangle GetRectangleMovedToCenter(Rectangle rect)
        {
            while (TryMoveToCenter(rect, out rect)) ;
            return rect;
        }

        private bool TryMoveToCenter(Rectangle rect, out Rectangle rectOut)
        {
            if (rect.X == center.X && rect.Y == center.Y)
            {
                rectOut = rect;
                return false;
            }

            var dx = center.X - rect.X;
            var dy = center.Y - rect.Y;

            rectOut = rect;
            var canMoveX = dx != 0 &&
                           (dx > 0
                               ? TryMove(rect, 1, 0, out rectOut)
                               : TryMove(rect, -1, 0, out rectOut));
            var canMoveY = dy != 0 &&
                           (dy > 0
                               ? TryMove(rectOut, 0, 1, out rectOut)
                               : TryMove(rectOut, 0, -1, out rectOut));
            return canMoveX || canMoveY;
        }

        private bool TryMove(Rectangle rect, int dx, int dy, out Rectangle rectOut)
        {
            rect.X += dx;
            rect.Y += dy;
            rectOut = rect;
            if (!Rectangles.Any(r => r.IntersectsWith(rect)))
                return true;

            rectOut.X -= dx;
            rectOut.Y -= dy;
            return false;
        }
    }
}