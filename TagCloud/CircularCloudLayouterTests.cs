using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloud
{
	[TestFixture]
	public class CircularCloudLayouterTests
	{
		[SetUp]
		public void SetUp()
		{
			var center = new Point(0,0);
			cloudLayouter = new CircularCloudLayouter(center);
		}

		[Test, Order(0)]
		public void PutNextRectangle_ReturnRectangleWithExactSize()
		{
			var size = new Size(29, 56);
			cloudLayouter.PutNextRectangle(size);
			var rectangle = cloudLayouter.Rectangles.First();
			rectangle.Size.Should().Be(size);
		}

		[Test, Order(1)]
		public void PutNextRectangle_SaveRectangle()
		{
			cloudLayouter.PutNextRectangle(GetRandomSize());
			cloudLayouter.Rectangles.Should().NotBeNullOrEmpty();
		}

		[Test, Order(2)]
		public void PutNextRectangle_AddSecondRectangle_PutItWithoutIntersection()
		{
			cloudLayouter.PutNextRectangle(GetRandomSize());
			cloudLayouter.PutNextRectangle(GetRandomSize());
			var intersection = AreRectanglesIntersect(cloudLayouter.Rectangles);
			intersection.Should().BeFalse();
		}

		[Test, Order(3)]
		public void PutNextRectangle_AddManyRectangles_ReturnExactCountOfRectangles()
		{
			var rectangleCount = 12;
			for (var i = 0; i < rectangleCount; i++)
				cloudLayouter.PutNextRectangle(GetRandomSize());
			cloudLayouter.Rectangles.Should().HaveCount(rectangleCount);
		}

		[Test, Order(4)]
		public void PutNextRectangle_AddManyRectangles_WithoutIntersection()
		{
			for (var i = 0; i < 20; i++)
				cloudLayouter.PutNextRectangle(GetRandomSize());
			var intersection = AreRectanglesIntersect(cloudLayouter.Rectangles);
			intersection.Should().BeFalse();
		}

		[Test, Order(5)]
		public void CircularCloudLayouter_HaveCircleShape()
		{
			for (var i = 0; i < 40; i++)
				cloudLayouter.PutNextRectangle(GetRandomSize());
			var radius = GetRadiusFromRectanglesSquare();
			cloudLayouter.Rectangles.All(rect => GetDistanceToCenter(rect) < radius).Should().BeTrue();
		}

		[Test, Order(6)]
		public void CircularCloudLayouter_CenterIsNotZero_HaveCircleShape()
		{
			var center = new Point(-100, 200);
			cloudLayouter = new CircularCloudLayouter(center);
			for (var i = 0; i < 20; i++)
				cloudLayouter.PutNextRectangle(GetRandomSize());
			var radius = GetRadiusFromRectanglesSquare();
			cloudLayouter.Rectangles.All(rect => GetDistanceToCenter(rect) < radius).Should().BeTrue();
		}

		[Test, Order(7)]
		public void PutNextRectangle_FillRectanglesDensely()
		{
			for (var i = 0; i < 20; i++)
				cloudLayouter.PutNextRectangle(GetRandomSize());
			var sumOfRectSquare = cloudLayouter.Rectangles.Select(x => x.Height * x.Width).Sum();
			var averageRadius = GetAverageRadius(cloudLayouter);
			var square = Math.PI * averageRadius * averageRadius;
			var density = sumOfRectSquare / square * 100;
			density.Should().BeInRange(60, 100);
		}
		
		[TestCase(10, 0, TestName = "Height are zero")]
		[TestCase(0, 10, TestName = "Width are zero")]
		[TestCase(0, 0, TestName = "Height and width are zero")]
		[TestCase(-10, 10, TestName = "Height are negative")]
		[TestCase(10, -10, TestName = "Width are negative")]
		[TestCase(-10, -10, TestName = "Height and width are negative")]
		public void PutNextRectangle_ZeroOrNegativeSize_ThrowException(int width, int height)
		{
			Action act = () => cloudLayouter.PutNextRectangle(new Size(width, height));
			act.Should().Throw<ArgumentException>();
		}

		
		[TearDown]
		public void TearDown()
		{
			if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
			{
				var name = TestContext.CurrentContext.Test.FullName;
				var path = Path.Combine(Environment.CurrentDirectory, name);
				var graphics = new Graphics();
				graphics.SaveMap(cloudLayouter.Rectangles, path);
				TestContext.Error.Write($@"Tag cloud visualization saved to file <{path}>");
			}
		}

		private CircularCloudLayouter cloudLayouter;
		public Random Random = new Random();

		private Size GetRandomSize()
		{
			return new Size(Random.Next(10, 50), Random.Next(10, 50));
		}

		private double GetDistanceToCenter(Rectangle rectangle)
		{
			var centerX = cloudLayouter.Center.X;
			var centerY = cloudLayouter.Center.Y;
			var x = rectangle.X - centerX;
			var y = rectangle.Y - centerY;
			return Math.Sqrt(x * x + y * y);
		}
		private bool AreRectanglesIntersect(IReadOnlyList<Rectangle> allRectangles)
		{
			for (var i = 0; i < allRectangles.Count; i++)
			{
				for (var j = i + 1; j < allRectangles.Count; j++)
				{
					if (allRectangles[i].IntersectsWith(allRectangles[j]))
						return true;
				}
			}
			return false;
		}

		private int GetRadiusFromRectanglesSquare()
		{
			var sumOfRectSquare = cloudLayouter.Rectangles.Select(x => x.Height * x.Width).Sum();
			return (int) Math.Sqrt(sumOfRectSquare * 2.5 / Math.PI);
		}

		private int GetAverageRadius(CircularCloudLayouter cloud)
		{
			var boundingCoordinate = new BoundingCoordinate(cloud.Rectangles);
			var radiusX = (boundingCoordinate.MaxX - boundingCoordinate.MinX) / 2;
			var radiusY = (boundingCoordinate.MaxY - boundingCoordinate.MinY) / 2;
			var averageRadius = (radiusY + radiusX) / 2;
			return averageRadius;
		}
	}
}