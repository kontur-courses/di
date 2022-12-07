using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.PointGenerators;
using TagCloud.Tag;

namespace TagCloudTests
{
    public class TagCloudTests
    {
        private CircularCloudLayouter cloudLayouter;
        private TagCloud.TagCloud tagCloud;

        [SetUp]
        public void PrepareCircularCloudLayouter()
        {
            var center = new Point();
            cloudLayouter = new CircularCloudLayouter(new SpiralPointGenerator(center));
            tagCloud = new TagCloud.TagCloud(center);

            var rectangle = new Rectangle(0, 0, 5, 5);
            tagCloud.Rectangles.Add(new Layout(rectangle));
        }

        [TestCase(0, 0, 35, 75, TestName = "center in zero point")]
        [TestCase(300, 300, 5, 5, TestName = "center in positive point")]
        [TestCase(-300, 300, 5, 5, TestName = "center in X negative point")]
        [TestCase(300, -300, 5, 5, TestName = "center in Y negative point")]
        [TestCase(-300, -300, 5, 5, TestName = "center in XY negative point")]
        public void PutNextRectangle_FirstRectangleMustBeInCenterOfCloud_When(int centerX, int centerY, int reactWidth, int reactHeight)
        {
            var center = new Point(centerX, centerY);
            cloudLayouter = new CircularCloudLayouter(new SpiralPointGenerator(center));
            var tagCloud = new TagCloud.TagCloud(center);

            var rectangle = cloudLayouter.PutNextRectangle(new Size(reactWidth, reactHeight));
            tagCloud.Rectangles.Add(new Layout(rectangle));
            var planningReactLocation = new Point(centerX - reactWidth / 2, centerY - reactHeight / 2);

            rectangle.Location.Should().BeEquivalentTo(planningReactLocation);

            tagCloud.GetHeight().Should().Be(reactHeight);
            tagCloud.GetWidth().Should().Be(reactWidth);
            tagCloud.GetLeftBound().Should().Be(planningReactLocation.X);
            tagCloud.GetTopBound().Should().Be(planningReactLocation.Y);
        }

        [Test]
        public void GetHashCode_Throw_NotImplementedException()
        {
            Action getHashCode = () => tagCloud.GetHashCode();

            getHashCode.Should().Throw<NotImplementedException>();
        }

        [Test]
        public void Equals_ReturnedTrue_ForEqualObjects()
        {
            var otherTagCloud = new TagCloud.TagCloud(tagCloud.Center);

            otherTagCloud.Rectangles.AddRange(tagCloud.Rectangles);

            tagCloud.Equals(otherTagCloud).Should().BeTrue();
        }

        [Test]
        public void Equals_ReturnedFalse_WhenCenterNotEquals()
        {
            var otherTagCloud = new TagCloud.TagCloud(new Point(tagCloud.Center.X -5, tagCloud.Center.Y - 7));

            tagCloud.Equals(otherTagCloud).Should().BeFalse();
        }

        [Test]
        public void Equals_ReturnedFalse_WhenRectanglesNotEquals()
        {
            var otherTagCloud = new TagCloud.TagCloud(tagCloud.Center);

            otherTagCloud.Rectangles.AddRange(
                tagCloud.Rectangles.Select(rectangle=>
                {
                    var newRectangle = new Rectangle(rectangle.Frame.Location, rectangle.Frame.Size);
                    newRectangle.Width += 5;
                    return new Layout(newRectangle);
                }));

            tagCloud.Equals(otherTagCloud).Should().BeFalse();
        }


        [Test]
        public void Equals_ReturnedFalse_WhenRectanglesCountNotEquals()
        {
            var otherTagCloud = new TagCloud.TagCloud(tagCloud.Center);
            
            otherTagCloud.Rectangles.AddRange(tagCloud.Rectangles);

            otherTagCloud.Rectangles.Add(tagCloud.Rectangles.Last());
            tagCloud.Equals(otherTagCloud).Should().BeFalse();

            otherTagCloud.Rectangles.Remove(otherTagCloud.Rectangles.Last());
            tagCloud.Equals(otherTagCloud).Should().BeTrue();

            tagCloud.Rectangles.Add(otherTagCloud.Rectangles.Last());
            tagCloud.Equals(otherTagCloud).Should().BeFalse();
        }
    }
}
