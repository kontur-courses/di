﻿using System;
using System.Linq;
using System.Drawing;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using Autofac;
using NUnit.Framework.Interfaces;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	[TestFixture]
	public class CircularCloudLayouter_Tests
	{
		private CircularCloudLayouter _circularCloudLayouter;
		private IContainer _container;

		[OneTimeSetUp]
		public void FirstSetUp()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<CircularCloudLayouter>().AsSelf();
			builder.RegisterType<ArchimedeSpiral>().As<ISpiral>();
			builder.RegisterType<SpiralSettings>().AsSelf();
			_container = builder.Build();
		}
		
		[SetUp]
		public void SetUp() => _circularCloudLayouter = _container.Resolve<CircularCloudLayouter>();

		[Test]
		public void PutNextRectangle_SavesPutRectangles()
		{
			const int expectedRectanglesCount = 5;

			for (var i = 0; i < expectedRectanglesCount; i++)
				_circularCloudLayouter.PlaceNextRectangle(new Size());
			var actualRectangles = _circularCloudLayouter.Rectangles;
			actualRectangles.Count.Should().Be(expectedRectanglesCount);
		}

		[TestCase(-1, 0, TestName = "Negative rectangle width")]
		[TestCase(0, -1, TestName = "Negative rectangle height")]
		[TestCase(-1, -1, TestName = "Negative rectangle width and height")]
		public void PutNextRectangle_ThrowsExceptionOnNegativeSizeValues(int width, int height)
		{
			var firstRectangleSize = new Size(width, height);
			Action action = () => _circularCloudLayouter.PlaceNextRectangle(firstRectangleSize);
			action.Should().Throw<ArgumentException>();
		}

		[TestCase(20, 20, TestName = "Squares")]
		[TestCase(50, 30, TestName = "Horizontal rectangles")]
		[TestCase(30, 50, TestName = "Vertical rectangles")]
		public void RectanglesShouldNotIntersect(int width, int height)
		{
			var rectangleSize = new Size(width, height);
			for (var i = 0; i < 20; i++)
				_circularCloudLayouter.PlaceNextRectangle(rectangleSize);
			
			CheckIntersection();
		}

		[Test]
		public void RectanglesShouldNotIntersect_WhenThereAreOnlyTwoSizes()
		{
			var maxSize = new Size(80, 40);
			var minSize = new Size(40, 20);
			var sizes = GenerateSizes(10, maxSize, minSize);
			foreach (var size in sizes)
				_circularCloudLayouter.PlaceNextRectangle(size);
			
			CheckIntersection();
		}

		private void CheckIntersection()
		{
			var rectangles = _circularCloudLayouter.Rectangles;
			for (var i = 0; i < rectangles.Count; i++)
			for (var j = i + 1; j < rectangles.Count; j++)
			{
				var intersects = rectangles[i].Intersects(rectangles[j]);
				Assert.IsFalse(intersects, $"{i + 1} intersects with {j + 1}");
			}
		}

		[Test]
		public void CloudShouldBeDense()
		{
			const double minimumAreaRatio = 0.5;
			var rectangleSize = new Size(20, 20);
			for (var i = 0; i < 100; i++)
				_circularCloudLayouter.PlaceNextRectangle(rectangleSize);
			var maxDistance = _circularCloudLayouter.Rectangles.Select(CalculateDistanceFromCenter).Max();
			var maxArea = Math.PI * maxDistance * maxDistance;
			
			var actualFilledArea = _circularCloudLayouter.Rectangles.Sum(rect => rect.Width * rect.Height);
			var actualDensityCoefficient = actualFilledArea / maxArea;
			actualDensityCoefficient.Should().BeGreaterOrEqualTo(minimumAreaRatio);
		}

		private static double CalculateDistanceFromCenter(Rectangle rectangle)
		{
			var rectangleCenter = rectangle.GetCenter();
			Point furtherPoint;
			if (rectangleCenter.X >= 0)
				furtherPoint = rectangleCenter.Y >= 0
					? new Point(rectangle.Right, rectangle.Y)
					: new Point(rectangle.Right, rectangle.Bottom);
			else
				furtherPoint = rectangleCenter.Y >= 0 
					? rectangle.Location
					: new Point(rectangle.X, rectangle.Bottom);
			return Math.Sqrt(Math.Pow(furtherPoint.X, 2) + Math.Pow(furtherPoint.Y, 2));
		}

		private static IEnumerable<Size> GenerateSizes(int count, Size maxSize, Size minSize)
		{
			for (var i = 0; i < count / 2; i++)
			{
				yield return maxSize;
				yield return minSize;
			}
		}
	}
}