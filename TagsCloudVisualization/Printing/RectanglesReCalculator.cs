using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ResultProject;

namespace TagsCloudVisualization.Printing
{
    internal class RectanglesReCalculator : IRectanglesReCalculator
    {
        public Result<IList<Rectangle>> RecalculateRectangles(IList<Rectangle> rectangles, Size defaultMaxSize)
        {
            return MoveToCenter(rectangles)
                .Then(rects => rects.Select(rect => rect.Translate(rects.GetCircumscribedSize(), defaultMaxSize)))
                .Then(rects => rects.ToList() as IList<Rectangle>);
        }

        public Result<IList<Rectangle>> MoveToCenter(IList<Rectangle> rectangles)
        {
            return rectangles.AsResult()
                .ThenFailIf(x => !x.Any(), "rectangles list is empty")
                .Then(rects => rects.ToList())
                .Then(rects => (rects, center: new Point(rects.First().X + rects.First().Width / 2, rects.First().Y + rects.First().Height / 2)))
                .Then(x => (x.rects, x.center, initialSize: x.rects.GetCircumscribedSize()))
                .Then(x => (x.rects, centersDelta: new Size(x.center.X - x.initialSize.Width / 2, x.center.Y - x.initialSize.Height / 2)))
                .Then(x => x.rects.Select(rect => rect.Move(-x.centersDelta.Width, -x.centersDelta.Height)))
                .Then(rects => rects.ToList() as IList<Rectangle>);
        }
    }
}