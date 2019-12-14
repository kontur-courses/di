using System;
using System.Drawing;
using System.Windows.Forms;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Drawers;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Layouters.CloudLayouters;
using TagsCloudVisualization.Painters;
using TagsCloudVisualization.Preprocessing;
using TagsCloudVisualization.Preprocessing.PreprocessActions;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.Text;
using TagsCloudVisualization.Text.TextReaders;
using TagsCloudVisualization.UI;
using TagsCloudVisualization.VisualizerActions.GuiActions;
using TagsCloudVisualization.WordStatistics;

namespace TagsCloudVisualization
{
    public static class TagCloudVisualizerEntryPoint
    {
        private static WindsorContainer container;

        [STAThread]
        public static void Main(string[] args)
        {
            container = new WindsorContainer();
            ConfigureContainer();
            container.Resolve<IVisualizer>().Start(args);
        }

        private static void ConfigureContainer()
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));

            container.Register(Component.For<ITextReader>().ImplementedBy<TxtFileReader>());
            container.Register(Component.For<ITextReader>().ImplementedBy<DocxFileReader>());


            container.Register(Component.For<IPreprocessor>().ImplementedBy<ToLowercasePreprocessor>());
            container.Register(Component.For<IPreprocessor>().ImplementedBy<RemoveNotWordsPreprocessor>());
            container.Register(Component.For<IPreprocessor>().ImplementedBy<RemoveNotNounsPreprocessor>());
            container.Register(Component.For<IPreprocessor>().ImplementedBy<RemoveIgnoredWords>());


            container.Register(Component.For<IStatisticsCollector>().ImplementedBy<WordCountCollector>());


            container.Register(Component.For<IWordSizeChooser>().ImplementedBy<WordCountSizeChooser>());


            container.Register(Component.For<WordPainter>().ImplementedBy<DefaultWordPainter>());


            container.Register(Component.For<WordDrawer>().ImplementedBy<DefaultWordDrawer>());


            container.Register(Component.For<CloudLayouterConfiguration>().UsingFactoryMethod(() =>
            {
                return new CloudLayouterConfiguration(() =>
                {
                    var appSettings = container.Resolve<AppSettings>();
                    var imageCenter = new Point(appSettings.ImageSettings.Width / 2,
                        appSettings.ImageSettings.Height / 2);
                    return new CircularCloudLayouter(imageCenter);
                });
            }));


            container.Register(Component.For<IGuiAction>().ImplementedBy<ImageSettingsSetGuiAction>());
            container.Register(Component.For<IGuiAction>().ImplementedBy<FontSetGuiAction>());
            container.Register(Component.For<IGuiAction>().ImplementedBy<PaletteSetGuiAction>());
            container.Register(Component.For<IGuiAction>().ImplementedBy<OpenTextFileGuiAction>());
            container.Register(Component.For<IGuiAction>().ImplementedBy<ImageSaveGuiAction>());
            container.Register(Component.For<IGuiAction>().ImplementedBy<RestrictionsSetGuiAction>());


            container.Register(Component.For<IVisualizer>().UsingFactoryMethod(() =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false); //because this should be called before initializing form
                var guiActions = container.ResolveAll<IGuiAction>();
                var appSettings = container.Resolve<AppSettings>();
                var tagCloud = container.Resolve<TagCloudContainer>();
                var visualizer = new GraphicalVisualizer(guiActions, appSettings, tagCloud);
                appSettings.CurrentInterface = visualizer;
                return visualizer;
            }));


            container.Register(Classes.FromThisAssembly().Where(x => x.IsClass));
        }
    }
}