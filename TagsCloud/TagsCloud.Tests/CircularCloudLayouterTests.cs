using System.Drawing;
using System.Drawing.Imaging;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloud.Core;
using TagsCloud.Core.Helpers;
using TagsCloud.Core.Layouters;

namespace TagsCloud.Tests;

[TestFixture]
public class CircularCloudLayouterTests
{
	[SetUp]
	public void SetUp()
	{
		layouter = new CircularCloudLayouter(Point.Empty);
		placedRectangles = new List<Rectangle>();
	}

	[TearDown]
	public void SaveLayoutAfterTestFail()
	{
		if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;
		if (placedRectangles.Count == 0) return;

		var occupiedSpace = GetOccupiedSpace(placedRectangles);

		var width = (int)Math.Ceiling(occupiedSpace.Width * 1.3);
		var height = (int)Math.Ceiling(occupiedSpace.Height * 1.3);

		var image = RectanglePainter.GetTagCloudImage(
			placedRectangles
			, new Size(width, height)
			, new Point(width / 2, height / 2));

		var failedTestName = TestContext.CurrentContext.Test.Name;
		var failedTestMethodName = TestContext.CurrentContext.Test.MethodName;

		var path = directoryImageSaver.Save(image, $"{failedTestMethodName} with {failedTestName}");
		var message = $"Tag cloud visualization saved to file {path}";

		Console.WriteLine(message);
	}

	private CircularCloudLayouter layouter;
	private List<Rectangle> placedRectangles;
	private readonly DirectoryImageSaver directoryImageSaver = new($"{Environment.CurrentDirectory}\\FailedTestsLayouts", ImageFormat.Png);

	[TestCaseSource(nameof(CentersSource))]
	public void Constructor_WithCorrectCenter_ShouldNotThrow(Point center)
	{
		var action = () => new CircularCloudLayouter(center);

		action.Should().NotThrow();
	}

	[TestCase(10, 10, TestName = "Square")]
	[TestCase(100, 10, TestName = "Horizontal oriented rectangle")]
	[TestCase(10, 100, TestName = "Vertical oriented rectangle")]
	public void PutNextRectangle_FirstRectangle_ShouldBeInCenter(int width, int height)
	{
		var rectangleSize = new Size(width, height);
		var actual = layouter.PutNextRectangle(rectangleSize);
		placedRectangles.Add(actual);
		var expected = RectangleCreator.GetRectangle(Point.Empty, rectangleSize);

		actual.Should().BeEquivalentTo(expected);
	}

	[TestCase(0, 0, TestName = "Zero size")]
	[TestCase(0, 10, TestName = "Zero width")]
	[TestCase(10, 0, TestName = "Zero height")]
	[TestCase(-10, -10, TestName = "Negative size")]
	[TestCase(-10, 10, TestName = "Negative width")]
	[TestCase(10, -10, TestName = "Negative height")]
	public void PutNextRectangle_IncorrectRectangleSize_ShouldThrowArgumentException(int width, int height)
	{
		var rectangleSize = new Size(width, height);
		var action = () => layouter.PutNextRectangle(rectangleSize);

		action.Should().NotThrow<ArgumentException>();
	}

	[TestCaseSource(nameof(SameRectangleSizesSource))]
	[TestCaseSource(nameof(RandomRectangleSizesSource))]
	public void PutNextRectangle_ShouldNotContainIntersection(List<Size> sizes)
	{
		placedRectangles = sizes.Select(size => layouter.PutNextRectangle(size)).ToList();

		var intersectionsCount = placedRectangles
			.Select((rectangle, i) =>
				placedRectangles
					.Where((_, j) => i != j)
					.Count(rectangle.IntersectsWith))
			.Sum();

		intersectionsCount.Should().Be(0);
	}

	[TestCaseSource(nameof(SameRectangleSizesSource))]
	[TestCaseSource(nameof(RandomRectangleSizesSource))]
	public void PutNextRectangle_CloudShouldBeDense(IEnumerable<Size> sizes)
	{
		placedRectangles = sizes.Select(size => layouter.PutNextRectangle(size)).ToList();

		var occupiedSpace = GetOccupiedSpace(placedRectangles);

		var rectanglesAreaSum = placedRectangles.Select(r => r.Width * r.Height).Sum();
		var occupiedArea = occupiedSpace.Width * occupiedSpace.Height;

		var freeAreaPercent = GetFreeAreaPercent(occupiedArea, rectanglesAreaSum);

		freeAreaPercent.Should().BeLessOrEqualTo(40);
	}

	[TestCaseSource(nameof(SameRectangleSizesSource))]
	[TestCaseSource(nameof(RandomRectangleSizesSource))]
	public void PutNextRectangle_CloudShouldBeLikeACircle(IEnumerable<Size> sizes)
	{
		placedRectangles = sizes.Select(size => layouter.PutNextRectangle(size)).ToList();

		var occupiedSpace = GetOccupiedSpace(placedRectangles);

		var inscribedCircleRadius = Math.Min(occupiedSpace.Width, occupiedSpace.Height) / 2;
		var inscribedCircleArea = Math.Pow(inscribedCircleRadius, 2) * Math.PI;
		var occupiedArea = occupiedSpace.Width * occupiedSpace.Height;

		var freeAreaPercent = GetFreeAreaPercent(occupiedArea, inscribedCircleArea);

		freeAreaPercent.Should().BeLessOrEqualTo(35);
	}

	private static double GetFreeAreaPercent(double occupiedArea, double effectiveArea)
	{
		return Math.Abs(occupiedArea - effectiveArea) * 100 / occupiedArea;
	}

	private static Rectangle GetOccupiedSpace(IEnumerable<Rectangle> rectangles)
	{
		var rectanglesList = rectangles.ToList();
		var minX = rectanglesList.Min(r => r.Left);
		var maxX = rectanglesList.Max(r => r.Right);
		var minY = rectanglesList.Min(r => r.Top);
		var maxY = rectanglesList.Max(r => r.Bottom);
		var width = maxX - minX;
		var height = maxY - minY;

		return new Rectangle(minX, minY, width, height);
	}

	//[Test] //Uncomment this for check image creation when test failed
	//public void AlwaysFallsTest_ShouldCreateImage()
	//{
	//	var rnd = new Random();

	//	for (var i = 0; i < 1_000; i++)
	//	{
	//		var scale = rnd.Next(1, 5);
	//		placedRectangles.Add(layouter.PutNextRectangle(new Size(30 * scale, 10 * scale)));
	//	}

	//	false.Should().Be(true);
	//}

	public static IEnumerable<TestCaseData> CentersSource()
	{
		yield return new TestCaseData(Point.Empty).SetName("Center at origin");
		yield return new TestCaseData(new Point(1, 1)).SetName("Center in the first quadrant");
		yield return new TestCaseData(new Point(-1, 1)).SetName("Center in the second quadrant");
		yield return new TestCaseData(new Point(-1, -1)).SetName("Center in the third quadrant");
		yield return new TestCaseData(new Point(1, -1)).SetName("Center in the fourth coordinate quadrant");
	}

	public static IEnumerable<TestCaseData> SameRectangleSizesSource()
	{
		var squareSize = new Size(30, 30);
		var verticalOrientedRectangleSize = new Size(10, 50);
		var horizontalOrientedRectangleSize = new Size(50, 10);

		yield return new TestCaseData(
				Enumerable.Repeat(squareSize, 100).ToList())
			.SetName("100 squares with same size");

		yield return new TestCaseData(
				Enumerable.Repeat(verticalOrientedRectangleSize, 100).ToList())
			.SetName("100 vertical oriented rectangles with same size");

		yield return new TestCaseData(
				Enumerable.Repeat(horizontalOrientedRectangleSize, 100).ToList())
			.SetName("100 horizontal oriented rectangles with same size");
	}

	public static IEnumerable<TestCaseData> RandomRectangleSizesSource()
	{
		var rnd = new Random();

		yield return new TestCaseData(GetRandomRectangles(100, 1, 1))
			.SetName("100 squares with random size");

		yield return new TestCaseData(GetRandomRectangles(100, 1, 3))
			.SetName("100 vertical oriented rectangles with random size");

		yield return new TestCaseData(GetRandomRectangles(100, 3, 1))
			.SetName("100 horizontal oriented rectangles with random size");
	}

	private static IEnumerable<Size> GetRandomRectangles(int count, int widthRate, int heightRate)
	{
		var rnd = new Random();

		return Enumerable.Repeat(0, count)
			.Select(_ => rnd.Next(10, 50))
			.Select(scale => new Size(widthRate * scale, heightRate * scale))
			.ToList();
	}
}