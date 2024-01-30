using System.Drawing;
using TagsCloudContainer;
using TagsCloudContainer.Cloud;

namespace TagsCloudContainerTests;

public class CircularCloudLayouterTest
{
    [TestCase(0, 0, 1)]
    [TestCase(2, 10, 2)]
    public void CorrectParameters_ShouldNotGiveErrors(int x, int y, double step)
    {
        Action action = () => new SpiralFunction(new Point(x, y), step);
        action.Should().NotThrow<ArgumentException>();
    }
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private Random _random = new(1);
        private Point _center;
        private static CircularCloudLayouter _layouter;
        private List<Rectangle> _rectangles = new();


        [SetUp]
        public void SetUp()
        {
            _center = new Point(0, 0);
            _layouter = new CircularCloudLayouter(_center);
        }

        [TestCase(0, 0)]
        [TestCase(-1, -1)]
        public void PutNextRectangle_ShouldThrowArgumentException_WhenIncorrectSize(int sizeX, int sizeY)
        {
            Action action = () => _layouter.PutNextRectangle(new Size(sizeX, sizeY));
            action.Should().Throw<ArgumentException>().WithMessage("Width and Height Size should positive");
        }

        [TestCase(10, 10, 10)]
        public void PutNextRectangle_AddRectangles(int sizeX, int sizeY, int count)
        {
            for (var i = 0; i < count; i++)
                _layouter.PutNextRectangle(new Size(sizeX, sizeY));
            
            _layouter._rectangles.Count.Should().Be(count);
        }

        [Test]
        public void CreateEmptyRectangle()
        {
            _layouter = new CircularCloudLayouter(new Point());
            _layouter._rectangles.Should().BeEmpty();
        }

        [TestCase(30)]
        [TestCase(50)]
        [TestCase(100)]
        public void PlacedRectangles_ShouldTrue_WhenCorrectNotIntersects(int countRectangles)
        {
            var isIntersectsWith = false;
            
            for (var i = 0; i < countRectangles; i++)
            {
                var size = new Size(_random.Next(50, 100), _random.Next(40, 80));
                var rectangle = _layouter.PutNextRectangle(size);
                if (rectangle.IsIntersectOthersRectangles(_rectangles))
                {
                    isIntersectsWith = true;
                }
                _rectangles.Add(rectangle);
            }
            isIntersectsWith.Should().BeTrue();
        }
    }
}