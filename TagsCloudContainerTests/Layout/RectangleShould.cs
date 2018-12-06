using System.Drawing;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudContainer.Extensions;


namespace TagsCloudVisualizationTests
{
    [TestFixture]
    public class RectangleShould
    {
        [Test]
        public void ReturnValidVertices()
        {
            var rectangle = new Rectangle(new Point(-10, 12), new Size(12, 9));

            rectangle.Vertices().Should().BeEquivalentTo(new[]
            {
                new Point(-10, 12),
                new Point(-10, 21),
                new Point(2, 12),
                new Point(2, 21),
            });
        }
    }
}
