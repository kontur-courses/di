using System.Linq;
using Autofac;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization;

namespace TagCloud
{
    class TagCloudApp
    {
        public static void Main(string[] args)
        {
            var container = new  ContainerBuilder();
            container.RegisterType<TagCloudCreator>()
                     .AsSelf();
            container.RegisterType<CircularCloudLayouter>()
                     .AsSelf();
            container.RegisterType<FileTextReader>()
                     .As<ITextReader>();
            container.RegisterType<SimpleWordsPreparer>()
                     .As<IWordsPreparer>();
            container.RegisterType<TagCloudStatsGenerator>()
                     .As<ITagCloudStatsGenerator>();
            container.RegisterType<FileSaver>()
                     .As<ITagCloudSaver>();
            container.RegisterType<UserInterface>()
                     .AsSelf();
            using (var scope = container.Build().BeginLifetimeScope())
            {
                var ui = scope.Resolve<UserInterface>();
                ui.Run(args);
            }

        }
    }
}
