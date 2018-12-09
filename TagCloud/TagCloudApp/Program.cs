using Autofac;
using TagCloudCreation;
using TagCloudVisualization;

namespace TagCloudApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var container = new ContainerBuilder();

            container.RegisterType<TagCloudCreator>()
                     .AsSelf();

            container.RegisterType<CircularCloudLayouter>()
                     .AsSelf();

            container.RegisterTypes(typeof(WhitespaceTextReader), typeof(NewLineTextReader))
                     .As<ITextReader>();

            container.RegisterType<RoundSpiralGenerator>()
                     .As<AbstractSpiralGenerator>();

            container.RegisterTypes(typeof(SimpleWordPreparer), typeof(BasicWordPreparer)) 
                     .As<IWordPreparer>();

            container.RegisterType<TagCloudStatsGenerator>()
                     .As<ITagCloudStatsGenerator>();

            container.RegisterType<ImageCreator>()
                     .As<ITagCloudImageCreator>();

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
