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

            container.RegisterTypes(typeof(WhitespaceTextReader)) //etc.
                     .As<ITextReader>();

            container.RegisterTypes(typeof(ShortWordDrawer), typeof(BasicDrawer))
                     .As<IWordDrawer>();

            container.RegisterType<CompositeDrawer>()
                     .AsSelf();

            container.RegisterType<RoundSpiralGenerator>()
                     .As<AbstractSpiralGenerator>();

            container.RegisterTypes(typeof(FixedBoringWordsRemover), typeof(LowerCaseSetter), typeof(Formatter),
                                    typeof(SelectedBoringWordsRemover), typeof(VerbRemover), typeof(PrepositionRemover))
                     .As<IWordPreparer>();

            container.RegisterType<TagCloudStatsGenerator>()
                     .As<ITagCloudStatsGenerator>();
            container.RegisterType<PathValidator>()
                     .As<IPathValidator>();
            container.RegisterType<TagCloudImageCreator>()
                     .AsSelf();

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
