using System.IO;
using System.Linq;
using Autofac;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using TagsCloudVisualization;

namespace TagCloud
{
    class TagCloudApp
    {
        private static readonly string[] BoringWords = {""};

        

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
            container.RegisterType<ConsoleUserInterface>()
                     .As<UserInterface>();
            container.Register(ctx=>new SimpleWordsPreparer(BoringWords))
                     .As<SimpleWordsPreparer>();
            using (var scope = container.Build().BeginLifetimeScope())
            {
                var ui = scope.Resolve<UserInterface>();
                ui.Run(args);
            }

        }
    }
}
