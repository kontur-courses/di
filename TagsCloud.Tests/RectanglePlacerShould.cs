using System.Drawing;
using TagsCloud.Creators.Implementation;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class RectanglePlacerShould
    {
        private RectangleCreator rectangleCreator;

        [SetUp]
        public void SetUp()
        {
            rectangleCreator = new RectangleCreator();
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.DefaultPointsAndSizeForPlace))]
        [Parallelizable(scope: ParallelScope.All)] 
        public Rectangle Place_DefaultPointsAndSize_MiddlePlace(int x, int y, int width, int height) =>
            rectangleCreator.Place(new Point(x, y), new Size(width, height));
    }
}