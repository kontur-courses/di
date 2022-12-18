using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.Distribution;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        [SetUp]
        public void SetUp()
        {
            center = new Point(Width / 2, Height / 2);
            spiral = new ArchimedeanSpiral(center);
            cloud = new CircularCloudLayouter(spiral);

            var bitmap = new Bitmap(Width, Height);
            var graphics = Graphics.FromImage(bitmap);

            word = "Слово";
            font = new Font("Arial", 20);
            size = graphics.MeasureString(word, font).Ceiling();
        }

        private const int Width = 800;
        private const int Height = 600;

        private Point center;
        private IDistribution spiral;
        private CircularCloudLayouter cloud;

        private string word;
        private Font font;
        private Size size;

        [Test]
        public void CircularCloudLayouter_DoesNotThrowException()
        {
            Action action = () => new CircularCloudLayouter(spiral);
            action.Should().NotThrow();
        }

        [TestCase(-1, -1, TestName = "Negative size")]
        [TestCase(-1, 0, TestName = "Negative width")]
        [TestCase(0, -1, TestName = "Negative height")]
        public void PutNextCloudItem_ThrowException_On(int width, int height)
        {
            Action action = () => cloud.PutNextCloudItem(word, new Size(width, height), font);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextCloudItem_ReturnsItemWithSameParameters()
        {
            var cloudItem = cloud.PutNextCloudItem(word, size, font);

            cloudItem.Font.Should().Be(font);
            cloudItem.Word.Should().Be(word);
            cloudItem.Rectangle.Size.Should().Be(size);
        }

        [Test]
        public void PutNextCloudItem_ShouldAddItemWithSameParameters()
        {
            cloud.PutNextCloudItem(word, size, font);

            cloud.Items.Last().Font.Should().Be(font);
            cloud.Items.Last().Word.Should().Be(word);
            cloud.Items.Last().Rectangle.Size.Should().Be(size);
        }

        [TestCase(1, 0, 1, TestName = "One item to empty cloud")]
        [TestCase(1, 1, 2, TestName = "One item to non-empty cloud ")]
        [TestCase(2, 0, 2, TestName = "Some item to empty cloud")]
        [TestCase(2, 2, 4, TestName = "Some item to non-empty cloud ")]
        public void PutNextCloudItem_ShouldAddItem(
            int count, int alreadyExist, int expectedExist)
        {
            for (var i = 0; i < alreadyExist; i++)
                cloud.PutNextCloudItem(word, size, font);
            cloud.Items.Should().HaveCount(alreadyExist);

            for (var i = 0; i < count; i++)
                cloud.PutNextCloudItem(word, size, font);
            cloud.Items.Should().HaveCount(expectedExist);
        }

        [TestCase(0, TestName = "Cloud does not contain items yet")]
        [TestCase(1, TestName = "Cloud already contains one item")]
        [TestCase(100, TestName = "Cloud already contains many items")]
        public void PutNextCloudItem_ShouldNotIntersectWithOtherItems_On(int alreadyExist)
        {
            for (var i = 0; i < alreadyExist; i++)
                cloud.PutNextCloudItem(word, size, font);

            var rectangle = cloud.PutNextCloudItem(word, size, font).Rectangle;
            cloud.Items.Take(alreadyExist)
                .Select(item => item.Rectangle)
                .Any(rect => rect.IntersectsWith(rectangle))
                .Should().BeFalse();
        }

        [TestCase(0, TestName = "Cloud does not contain items", ExpectedResult = false)]
        [TestCase(2, TestName = "Cloud contains two items", ExpectedResult = false)]
        [TestCase(100, TestName = "Cloud contains many items", ExpectedResult = false)]
        public bool CircularCloudLayouter_ItemsShouldNotIntersectWithEachOther_On(
            int alreadyExist)
        {
            for (var i = 0; i < alreadyExist; i++)
                cloud.PutNextCloudItem(word, size, font);

            for (var i = 0; i < cloud.Items.Count; i++)
            for (var j = i + 1; j < cloud.Items.Count; j++)
                if (cloud.Items[i].Rectangle.IntersectsWith(cloud.Items[j].Rectangle))
                    return true;

            return false;
        }
    }
}