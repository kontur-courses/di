using System;
using System.Drawing;
using NUnit.Framework;
using TagCloudApplication;
using TagCloudApplicationTest.ColorSchemes;
using TagCloudApplicationTest.Savers;
using TagCloudApplicationTest.TagCloudLayouters;
using TagCloudApplicationTest.WordKeepers;

namespace TagCloudApplicationTest
{
    [TestFixture]
    public class TagCloudCreatorUnitTest
    {
        [TestCase(300,300, TestName = "300x300")]
        [TestCase(500, 500, TestName = "500x500")]
        [TestCase(1000, 1000, TestName = "1000x1000")]
        [TestCase(150, 150, TestName = "150x150")]
        public void BuildTagCloud_ShouldBuildCorrectTagCloudByDifferentSquareImageSize(int width, int height)
        {
            var givenSize = new Size(width, height);
            var creator = new TagCloudCreator(new CircularCloudLayouter(new Point(0, 0)),
                new TestWordKeeper());
            
            var tagCloud = creator.BuildTagCloudBy("");
            tagCloud.SaveAsImage($"Test_BuildTagCloud1 {TestContext.CurrentContext.Test.Name}", givenSize, 0, new SimpleBMPSaver());
            //Assert.AreEqual(givenSize, tagCloud.ImageSize);
        }


        [TestCase(1000, 300, TestName = "1000x300")]
        [TestCase(200, 700, TestName = "200x700")]
        public void BuildTagCloud_ShouldBuildCorrectTagCloudByDifferentRectangleImageSize(int width, int height)
        {
            var givenSize = new Size(width, height);
            var creator = new TagCloudCreator(new CircularCloudLayouter(new Point(0, 0)),
                new TestWordkeeper2());

            var tagCloud = creator.BuildTagCloudBy("");
            tagCloud.SaveAsImage($"Test_BuildTagCloud2 {TestContext.CurrentContext.Test.Name}", givenSize, 0, new SimpleBMPSaver());
            //Assert.AreEqual(givenSize, tagCloud.ImageSize);
        }
    }
}
