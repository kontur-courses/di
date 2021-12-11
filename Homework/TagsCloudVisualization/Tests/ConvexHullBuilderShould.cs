using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class ConvexHullBuilderShould
    {
        [TestCaseSource(nameof(GetRotationDirectionTestData))]
        public int ReturnCorrectVectorToPointRotationDirection(Vector vector, Point point)
        {
            return ConvexHullBuilder.GetRotationDirection(vector, point);
        }

        [TestCaseSource(nameof(GetMinimalConvexHullTestData))]
        public void ReturnCorrectMinimalConvexHull(IReadOnlyCollection<Point> givenPoints,
            IReadOnlyCollection<Point> expectedConvexHull)
        {
            var actualConvexHull = ConvexHullBuilder.GetConvexHull(givenPoints);

            actualConvexHull.Should().BeEquivalentTo(expectedConvexHull);
        }

        [Test]
        public void ReturnCorrectRectanglePointsSet()
        {
            var rectangles = new List<Rectangle>
            {
                new Rectangle(new Point(0, 0), new Size(30, 20)),
                new Rectangle(new Point(-10, -10), new Size(10, 10)),
                new Rectangle(new Point(10, -10), new Size(40, 5))
            };
            var expectedPointsSet = new List<Point>
            {
                new Point(0, 0), new Point(0, 20), new Point(30, 0),
                new Point(30, 20), new Point(-10, -10), new Point(-10, 0),
                new Point(0, -10), new Point(10, -10), new Point(50, -10),
                new Point(50, -5), new Point(10, -5)
            };

            var actualPointsSet = ConvexHullBuilder.GetRectanglesPointsSet(rectangles);

            actualPointsSet.Should().BeEquivalentTo(expectedPointsSet);

        }

        private static IEnumerable<TestCaseData> GetMinimalConvexHullTestData()
        {
            yield return new TestCaseData(
                new List<Point>
                {
                    new Point(0, 0)
                },
                new List<Point>
                {
                    new Point(0,0)
                })
                .SetName("when one point is given");
            yield return new TestCaseData(
                new List<Point>
                {
                    new Point(0, 0), new Point(1,1)
                },
                new List<Point>
                {
                    new Point(0,0), new Point(1,1)
                })
                .SetName("when two poinst are given");
            yield return new TestCaseData(
                new List<Point>
                {
                    new Point(0, 0), new Point(1,1), new Point(2,2)
                },
                new List<Point>
                {
                    new Point(0,0), new Point(1,1), new Point(2,2)
                })
                .SetName("when three poinst are given");
            yield return new TestCaseData(
                new List<Point>
                {
                    new Point(2, 1), new Point(4, 1), new Point(6, 2), new Point(8, 4),
                    new Point(5, 5), new Point(3, 4), new Point(2, 3), new Point(4, 3),
                    new Point(6, 4), new Point(5, 3), new Point(5, 2), new Point(3, 2)
                },
                new List<Point>
                {
                    new Point(2, 1), new Point(4, 1), new Point(6, 2), new Point(8, 4),
                    new Point(5, 5), new Point(3, 4), new Point(2, 3),
                })
                .SetName("when points count more than three");
        }

        private static IEnumerable<TestCaseData> GetRotationDirectionTestData()
        {
            yield return new TestCaseData(
                new Vector(new Point(1, 1), new Point(4, 3)),
                new Point(3, 5))
                .Returns(1)
                .SetName("when point is located to the left of the vector");
            yield return new TestCaseData(
                new Vector(new Point(0, 0), new Point(3, 6)),
                new Point(4, -2))
                .Returns(-1)
                .SetName("when point is located to the right of the vector");
            yield return new TestCaseData(
                new Vector(new Point(0, 0), new Point(5, 5)),
                new Point(3, 3))
                .Returns(0)
                .SetName("when point is located on the vector");
        }
    }
}
