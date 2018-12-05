using Autofac;
using TagsCloudVisualization;

namespace TagCloud
{
    internal class TagCloudApp
    {
        private static readonly string[] BoringWords = {""};

        public static void Main(string[] args)
        {
            var container = new ContainerBuilder();
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
                     .As<UserInterface>()
                     .SingleInstance();

            using (var scope = container.Build()
                                        .BeginLifetimeScope())
            {
                var ui = scope.Resolve<UserInterface>();
                ui.Run(args);
            }
        }
    }
}
