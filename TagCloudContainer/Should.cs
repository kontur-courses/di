using Autofac;
using NUnit.Framework;
using System.Drawing;
using TagsCloudVisualization;

namespace TagCloudContainer
{
    [TestFixture]
    public class SimpleTagCloudShould
    {
        private IContainer? container;


        [OneTimeSetUp]
        public void Init()
        { /* ... */ }

        [Test]
        public void check_simple()
        {
            var path = "word_data\\data.txt";
            CreateTagCloud(new Point(0,0),path,"123");
        }

        public static void CreateTagCloud(Point center,string path,string nameSave)
        {
           
            var builder = new ContainerBuilder();
            builder.RegisterTypes(typeof(List<Point>), typeof(List<TextRectangle>), typeof(TagCloud)).AsSelf();
            builder.RegisterType<CircularCloudLayouter>().AsSelf();
            builder.Register(x => new CircularCloudLayouter(new FrequencyTags()
                .GetDictionaryWithTags(new FileParserForLines(path)
                    .GetWords()).DivideTags(), Graphics.FromImage(new Bitmap(1000, 1000))));
            builder.Register(e => new ArithmeticSpiral(center));
            var container = builder.Build();
            container.Resolve<TagCloud>().Save(nameSave);
        }
    }
}
