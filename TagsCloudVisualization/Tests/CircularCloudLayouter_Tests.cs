using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class CircularCloudLayouter_Tests
    {
        private CircularCloudLayouter layout;
        private Point cloudCenter;
        private string currentTestName => TestContext.CurrentContext.Test.Name;
        
        [SetUp]
        public void SetUp()
        {
            cloudCenter = new Point(100, 100);
            layout = new CircularCloudLayouter(cloudCenter);
        }
        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.FailCount == 0) return;

            var desktopPath = TestContext.CurrentContext.TestDirectory;
            var path = Path.Combine(desktopPath, currentTestName + ".bmp");

            var actualMaxRadius = 1;

            if (layout.Rectangles.Count != 0)
                actualMaxRadius = (int) layout.Rectangles.Max(rectangle => rectangle.DistanceTo(cloudCenter)) + 100;

            CloudTagDrawer.DrawRectanglesToFile(cloudCenter, layout.Rectangles.ToList(), path, 2*actualMaxRadius, 2*actualMaxRadius);

            Console.WriteLine($@"Tag cloud visualization saved to file {path}");
        }

        [TestCase(-1, 3, TestName = "Negative X coordinate")]
        [TestCase(1, -3, TestName = "Negative Y coordinate")]
        public void TestConstructor_ThrowArgumentException(int x, int y)
        {
            var cloudCenter = new Point(x, y);
            Assert.Throws<ArgumentException>(() => new CircularCloudLayouter(cloudCenter));
        }

        [TestCase(-10, 10, TestName = "Negative width")]
        [TestCase(10, -10, TestName = "Negative height")]
        [TestCase(0, 10, TestName = "Zero width")]
        [TestCase(10, 0, TestName = "Zero height")]
        public void PutNextRectangle_ThrowArgumentException_OnNonPositiveSize(int width, int height)
        {
            var size = new Size(width, height);
            Assert.Throws<ArgumentException>(() => layout.PutNextRectangle(size));
        }

        [Test]
        public void PutNextRectangle_ShouldReturnRectangle_WithCorrectSize()
        {
            layout.PutNextRectangle(new Size(20, 25));
            layout.Rectangles[0].Size.ShouldBeEquivalentTo(new Size(20, 25));
        }

        private static readonly object[] RectanglesCases =
        {
            new object[] { new []{(1,2), (3,4)}, 2},
            new object[] { new []{ (1, 2), (3, 4), (4, 2) }, 3},
            new object[] { new []{ (1, 2), (3, 4), (4, 2), (5, 7), (10, 15), (1, 2) }, 6},
        };

        

        [Test, TestCaseSource("RectanglesCases")]
        public void PutNextRectangle_AddManyRectangles((int, int)[] sizePairsArray, int expectedResult)
        {
            foreach (var sizePair in sizePairsArray){
                layout.PutNextRectangle(new Size(sizePair.Item1, sizePair.Item2));
            }
            layout.Rectangles.Count.Should().Be(expectedResult);
        }

        [Test]
        public void PutNextRectangle_FirstRectangle_ShouldBeOnCloudCenter()
        {
            layout.PutNextRectangle(new Size(10, 10));
            var firstRectangle = layout.Rectangles[0];
            var centerOfRectangle = new Point(firstRectangle.X + firstRectangle.Width / 2,
                firstRectangle.Y + firstRectangle.Height / 2);
            centerOfRectangle.ShouldBeEquivalentTo(cloudCenter);
        }

        [Test]
        public void PutNextRectangle_TwoAddedRectangles_ShouldNotIntersect()
        {
            layout.PutNextRectangle(new Size(20, 25));
            layout.PutNextRectangle(new Size(20, 25));
            var firstRectangle = layout.Rectangles[0];
            var secondRectangle = layout.Rectangles[1];
            firstRectangle.IntersectsWith(secondRectangle).Should().BeFalse();
        }

        [Test]
        public void PutNextRectangle_ShouldReturnRectanglesWithCorrectSize()
        {
            var size = new Size(20, 25);
            for (var i = 0; i < 100; i++)
               layout.PutNextRectangle(size);
            var isCorrectSize = layout.Rectangles.All(rectangle => Equals(rectangle.Size, size));
            isCorrectSize.Should().BeTrue();
        }


        [Test]
        public void PutNextRectangle_PairwiseIntersectionShouldBeFalse_OnBigNumberOfRectangles()
        {
            for (var i = 0; i < 100; i++)
                layout.PutNextRectangle(new Size(10, 10));

            PairwiseIntersection(layout.Rectangles.ToList()).Should().BeTrue();
        }

        [Test, Timeout(1000)]
        public void Timeout_AddALotOfRectangles()
        {
            for (var i = 1; i < 400; i++)
              layout.PutNextRectangle(new Size(10, 10));
        }

        [Test]
        public void PutNextRectangle_AllRectanglesShouldBeInTheFormOfNotBigCircle()
        {
            const int rectangleCount = 100;
            const int rectangleSize = 10;
            // Общая площадь всех прямоугольников = 100*10*10 
            // Радиус круга с такой площадью есть sqrt(100*10*10/pi)
            // Так как Облако из прямоугольников, возьмем 1.5 таких радиуса

            var maxDistance = 0.0;
            for (var i = 0; i < rectangleCount; i++)
            {
                layout.PutNextRectangle(new Size(rectangleSize, rectangleSize));
                var rectangle = layout.Rectangles[layout.Rectangles.Count-1];
                var distance = rectangle.DistanceTo(cloudCenter);
                if (distance > maxDistance)
                    maxDistance = distance;
            }
            var expectedRadius = 1.5*Math.Sqrt(rectangleCount *rectangleSize * rectangleSize / Math.PI);
            maxDistance.Should().BeLessThan(expectedRadius);
        }



        private static bool PairwiseIntersection(List<Rectangle> rectangleList)
        {
            for (var i = 0; i < rectangleList.Count; i++)
                for (var j = i + 1; j < rectangleList.Count; j++)
                    if (rectangleList[j].IntersectsWith(rectangleList[i]))
                        return true;
            return false;
        }
        
    }
    
}
