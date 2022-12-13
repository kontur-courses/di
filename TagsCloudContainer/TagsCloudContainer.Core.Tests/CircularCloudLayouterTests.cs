using System.Drawing;
using FluentAssertions;
using TagsCloudContainer.Core.Layouter;


namespace TagsCloudContainer.Core.Tests
{
    public class Tests
    {
        private Point _center;
        private CircularCloudLayouter _sut;

        [SetUp]
        public void Setup()
        {
            var options = new TestOptions(500, 500);
            _center = new Point(options.Width / 2, options.Height / 2);

            _sut = new CircularCloudLayouter();
            _sut.SetCenter(new Point(_center.X, _center.Y));
        }

        [Test]
        public void PutNextRectangle_FirstRectangle_ShouldBeCentral()
        {
            _sut.PutNextRectangle(new Size(10, 10)).Location
                .Should()
                .Be(new Point(_center.X - 5, _center.Y - 5));
        }

        [Test]
        public void PutNextRectangle_RectanglesIntersection_ShouldNotIntersect()
        {
            var rnd = new Random();

            var rectangles = Enumerable
                .Range(0, 200)
                .Select(_ => new Size(rnd.Next(100, 200), rnd.Next(50, 100)))
                .Select(size => _sut.PutNextRectangle(size))
                .ToList();

            for (var i = 0; i < rectangles.Count; i++)
                for (var j = i + 1; j < rectangles.Count; j++)
                    rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
            
        }

        [Test]
        [TestCase(-1, -1)]
        [TestCase(0, 0)]
        public void PutNextRectangle_TryPutNonPositiveRectangle_ShouldThrow(int width, int height)
        {
            var action = () => { _sut.PutNextRectangle(new Size(width, height)); };
            action.Should().Throw<ArgumentException>()
                            .WithMessage("Rectangle size is not positive.");
        }

        [Test]
        public void PutNextRectangle_CorrectRectSizeAfterInsertion_ShouldBeEqual()
        {
            var rnd = new Random();
            for (var i = 0; i < 100; i++)
            {
                var size = new Size(rnd.Next(10, 100), rnd.Next(10, 100));
                _sut.PutNextRectangle(size).Size.Should().Be(size);
            }
        }

        [Test]
        public void Rectangles_ShouldBeInsideCircle()
        {
            var random = new Random();
            var rectangles = Enumerable
                .Range(0, 200)
                .Select(_ => new Size(random.Next(10, 100), random.Next(10, 100)))
                .Select(size => _sut.PutNextRectangle(size))
                .ToList();

            var radius = GetCircleRadius(rectangles);

            foreach (var distanceToCenter in rectangles.Select(rectangle => GetMaximumDistance(rectangle, _center)))
            {
                distanceToCenter.Should().BeLessThan(radius);
            }
        }

        private static int GetCircleRadius(IEnumerable<Rectangle> rectangles)
        {
            const double radiusMultiplier = 1.25;

            var square = rectangles
                .Select(rectangle => rectangle.Width * rectangle.Height)
                .Sum();

            return (int)(Math.Sqrt(square / Math.PI) * radiusMultiplier);
        }

        private static double GetMaximumDistance(Rectangle rect, Point center)
        {
            var maxX = Math.Max(Math.Abs(center.X - rect.X), Math.Abs(center.X - rect.X - rect.Width));
            var maxY = Math.Max(Math.Abs(center.Y - rect.Y), Math.Abs(center.Y - rect.Y - rect.Height));

            return Math.Sqrt(maxX * maxX + maxY * maxY);
        }
    }
}