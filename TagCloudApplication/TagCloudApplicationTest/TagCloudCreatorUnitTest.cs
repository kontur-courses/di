using System;
using System.Drawing;
using Castle.Windsor;
using NUnit.Framework;
using TagCloudApplication;
using TagCloudApplication.Readers;
using TagCloudApplication.Savers;
using TagCloudApplication.WordKeepers;
using TagCloudApplicationTest.TagCloudLayouters;
using Component = Castle.MicroKernel.Registration.Component;

namespace TagCloudApplicationTest
{
    [TestFixture]
    public class TagCloudCreatorUnitTest
    {

        private WindsorContainer container = new WindsorContainer();

        [OneTimeSetUp]
        public void SetUp()
        {
            container.Register(Component.For<IReader>().ImplementedBy<TXTReader>());
            container.Register(Component.For<ISaver>().ImplementedBy<BMPSaver>());
            container.Register(Component.For<IWordKeeper>()
                .ImplementedBy<StandartWordKeeper>()
                .DependsOn(new {delimiters = new [] {"\n"}})
                .LifeStyle.Transient);
            container.Register(Component.For<ICloudLayouter>()
                .ImplementedBy<CircularCloudLayouter>()
                .DependsOn(new {pointCenter = new Point(0, 0)})
                .LifeStyle.Transient);

        }

        [TestCase(300,300, TestName = "300x300")]
        [TestCase(500, 500, TestName = "500x500")]
        [TestCase(1000, 1000, TestName = "1000x1000")]
        [TestCase(1000, 1000, TestName = "1800x1800")]
        [TestCase(150, 150, TestName = "150x150")]
        public void BuildTagCloud_ShouldBuildCorrectTagCloudByDifferentSquareImageSize(int width, int height)
        {
            var givenSize = new Size(width, height);
            var creator = new TagCloudCreator(container.Resolve<ICloudLayouter>(), container.Resolve<IWordKeeper>(), givenSize);

            var tagCloud = creator.BuildTagCloudBy(AppContext.BaseDirectory + "bigtext.txt");
            tagCloud.SaveAsImage($"{AppContext.BaseDirectory}Test_BuildTagCloud1 {TestContext.CurrentContext.Test.Name}", container.Resolve<ISaver>());
        }


        [TestCase(1000, 300, TestName = "1000x300")]
        [TestCase(200, 700, TestName = "200x700")]
        public void BuildTagCloud_ShouldBuildCorrectTagCloudByDifferentRectangleImageSize(int width, int height)
        {
            var givenSize = new Size(width, height);
            var creator = new TagCloudCreator(container.Resolve<ICloudLayouter>(), container.Resolve<IWordKeeper>(), givenSize);

            var tagCloud = creator.BuildTagCloudBy(AppContext.BaseDirectory + "bigtext.txt");
            tagCloud.SaveAsImage($"{AppContext.BaseDirectory}Test_BuildTagCloud2 {TestContext.CurrentContext.Test.Name}", container.Resolve<ISaver>());  
        }

    }
}
