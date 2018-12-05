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
            container.RegisterType<TagCloudOptions>()
                     .AsSelf();
            container.RegisterType<SquareSpiralGenerator>()
                     .As<ISpiralGenerator>();
            container.RegisterType<ConsoleUserInterface>()
                     .As<UserInterface>()
                     .SingleInstance();
            container.Register(ctx => new RoundSpiralGenerator(ctx.Resolve<TagCloudOptions>()
                                                                  .Center,
                                                               ctx.Resolve<TagCloudOptions>()
                                                                  .SizeCoefficient))
                     .As<ISpiralGenerator>();
            container.Register(ctx => new SimpleWordsPreparer(BoringWords))
                     .As<SimpleWordsPreparer>();

            using (var scope = container.Build()
                                        .BeginLifetimeScope())
            {
                var ui = scope.Resolve<UserInterface>();
                ui.Run(args);
            }
        }
    }
}
