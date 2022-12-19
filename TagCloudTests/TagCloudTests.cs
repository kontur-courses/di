using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.CloudLayouters;
using TagCloud.PointGenerators;
using TagCloud.Tags;

namespace TagCloudTests
{
    public class TagCloudTests
    {
        private ICloudLayouter cloudLayouter;
        private TagCloud.TagCloud tagCloud;

        public bool IsEquals(TagCloud.TagCloud firsTagCloud, TagCloud.TagCloud secondTagCloud)
        {
            return firsTagCloud != null &&
                   secondTagCloud != null &&
                   firsTagCloud.Center == secondTagCloud.Center &&
                   firsTagCloud.Layouts.Count == secondTagCloud.Layouts.Count &&
                   firsTagCloud.Layouts.TrueForAll(rectangle =>
                       secondTagCloud.Layouts.Contains(rectangle));
        }

        [SetUp]
        public void PrepareCircularCloudLayouter()
        {
            var center = new Point();
            cloudLayouter = new CircularCloudLayouter(() => new SpiralPointGenerator(center));
            tagCloud = new TagCloud.TagCloud(center);

            var rectangle = new Rectangle(0, 0, 5, 5);
            tagCloud.Layouts.Add(new Layout(rectangle));
        }

        [TestCase(0, 0, 35, 75, TestName = "center in zero point")]
        [TestCase(300, 300, 5, 5, TestName = "center in positive point")]
        [TestCase(-300, 300, 5, 5, TestName = "center in X negative point")]
        [TestCase(300, -300, 5, 5, TestName = "center in Y negative point")]
        [TestCase(-300, -300, 5, 5, TestName = "center in XY negative point")]
        public void PutNextRectangle_FirstRectangleMustBeInCenterOfCloud_When(int centerX, int centerY, int reactWidth, int reactHeight)
        {
            var center = new Point(centerX, centerY);
            cloudLayouter = new CircularCloudLayouter(() => new SpiralPointGenerator(center));
            var localTagCloud = new TagCloud.TagCloud(center);

            var rectangle = cloudLayouter.PutNextRectangle(new Size(reactWidth, reactHeight));
            localTagCloud.Layouts.Add(new Layout(rectangle));
            var planningReactLocation = new Point(centerX - reactWidth / 2, centerY - reactHeight / 2);

            rectangle.Location.Should().BeEquivalentTo(planningReactLocation);

            localTagCloud.GetHeight().Should().Be(reactHeight);
            localTagCloud.GetWidth().Should().Be(reactWidth);
            localTagCloud.GetLeftBound().Should().Be(planningReactLocation.X);
            localTagCloud.GetTopBound().Should().Be(planningReactLocation.Y);
        }

        [Test]
        public void Equals_ReturnedTrue_ForEqualObjects()
        {
            var otherTagCloud = new TagCloud.TagCloud(tagCloud.Center);

            otherTagCloud.Layouts.AddRange(tagCloud.Layouts);

            IsEquals(tagCloud, otherTagCloud).Should().BeTrue();
        }

        [Test]
        public void Equals_ReturnedFalse_WhenCenterNotEquals()
        {
            var otherTagCloud = new TagCloud.TagCloud(new Point(tagCloud.Center.X -5, tagCloud.Center.Y - 7));

            IsEquals(tagCloud, otherTagCloud).Should().BeFalse();
        }

        [Test]
        public void Equals_ReturnedFalse_WhenRectanglesNotEquals()
        {
            var otherTagCloud = new TagCloud.TagCloud(tagCloud.Center);

            otherTagCloud.Layouts.AddRange(
                tagCloud.Layouts.Select(rectangle=>
                {
                    var newRectangle = new Rectangle(rectangle.Frame.Location, rectangle.Frame.Size);
                    newRectangle.Width += 5;
                    return new Layout(newRectangle);
                }));

            IsEquals(tagCloud, otherTagCloud).Should().BeFalse();
        }


        [Test]
        public void Equals_ReturnedFalse_WhenRectanglesCountNotEquals()
        {
            var otherTagCloud = new TagCloud.TagCloud(tagCloud.Center);
            
            otherTagCloud.Layouts.AddRange(tagCloud.Layouts);

            otherTagCloud.Layouts.Add(tagCloud.Layouts.Last());
            IsEquals(tagCloud, otherTagCloud).Should().BeFalse();

            otherTagCloud.Layouts.Remove(otherTagCloud.Layouts.Last());
            IsEquals(tagCloud, otherTagCloud).Should().BeTrue();

            tagCloud.Layouts.Add(otherTagCloud.Layouts.Last());
            IsEquals(tagCloud, otherTagCloud).Should().BeFalse();
        }
    }
}
