using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ResultProject;

namespace TagsCloudVisualization.Printing
{
    public class RectanglesReCalculator : IRectanglesReCalculator
    {
        public Result<IList<Rectangle>> RecalculateRectangles(IList<Rectangle> rectangles, Size defaultMaxSize)
        {
            return MoveToCenter(rectangles)
                .Then(y => y.Select(x => x.Translate(y.GetCircumscribedSize(), defaultMaxSize)).ToList() as IList<Rectangle>);
            
            
            // var centeredRects = MoveToCenter(rectangles);
            // var oldSize = centeredRects.GetCircumscribedSize();
            // return centeredRects.Select(x => x.Translate(oldSize, defaultMaxSize)).ToList();
        }

        public Result<IList<Rectangle>> MoveToCenter(IList<Rectangle> rectangles)
        {
            if (!rectangles.Any()) return Result.Fail<IList<Rectangle>>($"rectangles list is empty");
            
            return Result.Ok((rectangles, rectangles.First()))
                .Then(x => (x.rectangles, x.Item2, new Point(x.Item2.X + x.Item2.Width / 2, x.Item2.Y + x.Item2.Height / 2)))
                .Then(x => (x.rectangles, x.Item2, x.Item3, x.rectangles.GetCircumscribedSize()))
                .Then(x => new Size(x.Item3.X - x.Item4.Width / 2, x.Item3.Y - x.Item4.Height / 2))
                .Then(y => rectangles.Select(x => x.Move(-x.Width, -x.Height)))
                .Then(x => x.ToList() as IList<Rectangle>);
            // var center = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
            // var initialSize = rectangles.GetCircumscribedSize();
            // var centersDelta = new Size(center.X - initialSize.Width / 2, center.Y - initialSize.Height / 2);

            // return rectangles.Select(x => x.Move(-centersDelta.Width, -centersDelta.Height)).ToList();
        }
    }
}