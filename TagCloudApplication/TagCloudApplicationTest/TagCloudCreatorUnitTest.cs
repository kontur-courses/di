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
        [Test]
        public void BuildTagCloud_ShouldBuildTagCloudByGivenSize()
        {
            var givenSize = new Size(300, 300);
            var creator = new TagCloudCreator(new CircularCloudLayouter(new Point(0, 0)),
                new TestWordKeeper(),givenSize);
            
            var tagCloud = creator.BuildTagCloudBy("");
            tagCloud.ApplyColorScheme(new SimpleRandomColorScheme()).SaveAsImage("testTagCloud", new SimpleBMPSaver());
            Assert.AreEqual(givenSize, tagCloud.ImageSize);
        }
    }
}
