using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Interfaces;
using TagsCloud.Extensions;

namespace TagsCloud.CloudLayouters
{
    public class CircularCloudLayouter : ITagCloudLayouter
    {
        private readonly Point center = new Point(0, 0);
        private readonly List<Rectangle> rectangles;
        private readonly IPositionGenerator positionGenerator;

        public CircularCloudLayouter(IPositionGenerator positionGenerator)
        {
            rectangles = new List<Rectangle>();
            this.positionGenerator = positionGenerator;
        }

        public CircularCloudLayouter()
        {
            rectangles = new List<Rectangle>();
            positionGenerator = new RoundSpiralPositionGenerator();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width < 0 || rectangleSize.Height < 0)
            {
                throw new ArgumentException("The rectangle cannot have a negative length side");
            }
            var nextPosition = positionGenerator.GetNextPosition();
            var rectangle = new Rectangle(nextPosition, rectangleSize);
            while (IntersectsWithPrevious(rectangle))
            {
                nextPosition = positionGenerator.GetNextPosition();
                rectangle.MoveToPosition(nextPosition);
            }
            if (rectangles.Count > 0)
                OptimizeLocation(ref rectangle);
            rectangles.Add(rectangle);
            return rectangle;
        }

        private void OptimizeLocation(ref Rectangle rectangle)
        {
            Func<Rectangle, int> getDeltaYByCenterCoordinate = (Rectangle rect) => center.Y - rect.Y;
            Func<Rectangle, int> getDeltaXByCenterCoordinate = (Rectangle rect) => center.X - rect.X;
            var wathOptimize = true;
            while(wathOptimize)
            {
                TryOptimizeOneCoordinate(ref rectangle, getDeltaYByCenterCoordinate, 0, Math.Sign(rectangle.Y - center.Y) * -1);
                wathOptimize = TryOptimizeOneCoordinate(ref rectangle, getDeltaXByCenterCoordinate, Math.Sign(rectangle.X - center.X) * -1, 0);
            }
        }

        private bool TryOptimizeOneCoordinate(ref Rectangle rectangle, Func<Rectangle, int> getDeltaByCenterCoordinate, int deltaXByStep, int deltaYByStep)
        {
            var rndStop = new Random().Next(100, 200);
            var onCenter = false;
            var countMove = 0;
            while (!IntersectsWithPrevious(rectangle))
            {
                if (Math.Abs(getDeltaByCenterCoordinate(rectangle)) < rndStop)
                {
                    onCenter = true;
                    break;
                }
                rectangle.Move(deltaXByStep, deltaYByStep);
                countMove++;
            }
            if (!onCenter)
                rectangle.Move(-deltaXByStep, -deltaYByStep);
            return countMove > 1;
        }

        private bool IntersectsWithPrevious(Rectangle rectangle)
        {
            return rectangles.Any(previousRectangle => previousRectangle.IntersectsWith(rectangle));
        }
    }
}
