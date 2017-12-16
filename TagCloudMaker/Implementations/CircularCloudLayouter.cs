using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Interfaces;

namespace TagCloud.Implementations
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly List<TextRectangle> rectangles;
        public TextRectangle[] CloudRectangles => rectangles.ToArray();
        private readonly IPointComputer pointComputer;

        public CircularCloudLayouter(IPointComputer computer)
        {
            rectangles = new List<TextRectangle>();
            pointComputer = computer;
        }

        public Result<None> PutNextRectangle(Size rectangleSize, string text)
        {
            var result = GetNextRectangle(rectangleSize, text);
            if (!result.IsSuccess)
                return Result.Fail<None>(result.Error);

            while (rectangles.Any(tr => tr.Rectangle.IntersectsWith(result.Value.Rectangle)))
            {
                result = GetNextRectangle(rectangleSize, text);
                if (!result.IsSuccess)
                    return Result.Fail<None>(result.Error);
            }

            rectangles.Add(result.Value);
            return Result.Ok();
        }
        
        private Result<TextRectangle> GetNextRectangle(Size rectangleSize, string text)
        {
            Result<Point> result = rectangles.Count == 0
                ? pointComputer.GetNextPoint(0, 0)
                : pointComputer.GetNextPoint(0.1, 50);
            if (!result.IsSuccess)
                return Result.Fail<TextRectangle>(result.Error);

            var location = result.Value.GetLeftTopCorner(rectangleSize);
            return Result.Ok(new TextRectangle(location, rectangleSize, text));
        }
    }
}