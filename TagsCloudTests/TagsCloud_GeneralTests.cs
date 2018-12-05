using System.Drawing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Layouter;
using Point = TagsCloudVisualization.Layouter.Point;

namespace TagsCloudTests
{
    [TestFixture]
    public class TagsCloud_GeneralTests
    {
        [Test]
        public void GenerateFromFile()
        {
            var origin = new Point(0, 0);
            var container = new WindsorContainer();
            container.Register(Component.For<IFileReader>().ImplementedBy<FileReader>());
            container.Register(Component.For<CircularCloudLayouter>().ImplementedBy<CircularCloudLayouter>()
                .DependsOn(Dependency.OnValue("origin", origin)));
            container.Register(Component.For<TagsCloud>().ImplementedBy<TagsCloud>());
            container.Register(Component.For<Point>().ImplementedBy<Point>());
            var tagsCloud = container.Resolve<TagsCloud>(new Point(0, 0));
            tagsCloud.AddWordsFromFile("input.txt");
            tagsCloud.AddStopWords("stopwords.txt");
            tagsCloud.SetColor(Color.Red);
            tagsCloud.Compile("result.png");
        }
    }
}