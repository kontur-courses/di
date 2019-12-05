using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var path = "words.txt";
            var size = new Size(2000, 2000);
            var container = new WindsorContainer();
            container.Register(Component.For<TagsCloudContainer>());
            container.Register(Component.For<ITextReader>()
                .ImplementedBy<SimpleTextReader>()
                .DependsOn(
                    Dependency.OnValue("path", path)
                    ));
            var reader = container.Resolve<ITextReader>();
            container.Register(Component.For<IWordsFilter>()
                .ImplementedBy<SimpleWordsFilter>()
                .DependsOn(
                    Dependency.OnValue("arr", reader.Read().ToArray())
                ));
            var filter = container.Resolve<IWordsFilter>();
            container.Register(Component.For<IWordsCounter>()
                .ImplementedBy<SimpleWordsCounter>()
                .DependsOn(
                    Dependency.OnValue("arr", filter.FilterWords().ToArray())
                ));
            var counter = container.Resolve<IWordsCounter>();
            container.Register(Component.For<IWordsToSizesConverter>()
                    .ImplementedBy<SimpleWordsToSizesConverter>()
                    .DependsOn(
                        Dependency.OnValue("size", size),
                        Dependency.OnValue("dictionary",
                            counter.CountWords().ToDictionary(kvp => kvp.Key, kvp => kvp.Value)),
                        Dependency.OnValue("sizeOfLayout", size)));
            container.Register(Component.For<ICircularCloudLayouter>()
                .ImplementedBy<CircularCloudLayouter>()
                .DependsOn(Dependency.OnValue("center", new Point(size.Width/2, size.Height/2))
            ));
            container.Register(Component.For<IVisualiser>()
                .ImplementedBy<SimpleRectanglesVisualiser>()
            );
            var tagsContainer = container.Resolve<TagsCloudContainer>();
            tagsContainer.Perform();
        }
    }
}