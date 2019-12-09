using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;
using System;
using System.IO;
using TagCloud.Actions;
using TagCloud.IServices;

namespace TagCloud
{
    public class Program
    {
        public static void Main()
        {
            var container = GetContainer();
            var visaulization = container.Resolve<ICloudVisualization>();
            var saveAction = container.Resolve<ISaveImageAction>();
            while (true)
            {
                var width = int.Parse(Console.ReadLine() ?? throw new ArgumentException());
                var height = int.Parse(Console.ReadLine() ?? throw new ArgumentException());
                var pathToRead = Console.ReadLine();
                var pathToSave = Path.GetTempPath()+"testCloud1";
                saveAction.Perform(pathToSave,visaulization.GetAndDrawRectangles(width,height,pathToRead));
            }
        }

        private static WindsorContainer GetContainer()
        {
            var container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();
            container.Register(Component.For<IWordsToTagsParser>().ImplementedBy<WordsToTagsParser>());
            container.Register(Component.For<IWordsHandler>().ImplementedBy<WordsHandler>());
            container.Register(Component.For<ITagCollectionFactory>().ImplementedBy<TagCollectionFactory>()
                .LifestyleSingleton());
            container.Register(Component.For<IAlgorithm>().ImplementedBy<ArchimedianSprial>());
            container.Register(Component.For<ICircularCloudLayouter>().ImplementedBy<CircularCloudLayouter>());
            container.Register(Component.For<ICloud>().ImplementedBy<Cloud>());
            container.Register(Component.For<ICloudVisualization>().ImplementedBy<CloudVisualization>());
            container.Register(Component.For<ISaveImageAction>().ImplementedBy<SaveImageAction>());
            container.Register(Component.For<IAction>().ImplementedBy<ShowImageAction>());

            return container;
        }
    }
}
