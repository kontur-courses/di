using System.Drawing;
using TagsCloud.Core.Helpers;
using TagsCloud.Core.Layouters.Spirals;

namespace TagsCloud.Core.Layouters;

public class SpiralCloudLayouter : ICloudLayouter
{
	private readonly List<Rectangle> placedRectangles;

	private readonly ISpiral spiral;

	public SpiralCloudLayouter(Point center, double spiralStepInPixels, double angleStepInDegrees)
	{
		spiral = new ArchimedeanSpiral(center, spiralStepInPixels, angleStepInDegrees);
		placedRectangles = new List<Rectangle>();
	}

	public Rectangle PutNextRectangle(Size rectangleSize)
	{
		var rectangleOnSpiral = GetRectangleOnSpiral(rectangleSize);

		placedRectangles.Add(rectangleOnSpiral);

		return rectangleOnSpiral;
	}

	private Rectangle GetRectangleOnSpiral(Size rectangleSize)
	{
		Rectangle newRectangle;

		do
		{
			newRectangle = RectangleCreator.GetRectangle(spiral.GetNextPoint(), rectangleSize);
		} while (IntersectsWithPlacedRectangles(newRectangle));

		return newRectangle;
	}

	private bool IntersectsWithPlacedRectangles(Rectangle rectangle)
	{
		return placedRectangles.Any(placedRectangle => placedRectangle.IntersectsWith(rectangle));
	}
}