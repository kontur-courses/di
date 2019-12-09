using System;
using System.Drawing;
using System.Windows.Forms;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using TagsCloudVisualization.Drawers;
using TagsCloudVisualization.GUI;
using TagsCloudVisualization.GUI.GuiActions;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Layouters.CloudLayouters;
using TagsCloudVisualization.Painters;
using TagsCloudVisualization.Preprocessing;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.Text;
using TagsCloudVisualization.Text.TextReaders;
using TagsCloudVisualization.VisualizerActions.GuiActions;
using TagsCloudVisualization.WordStatistics;

namespace TagsCloudVisualization
{
    public static class TagCloudVisualizer
    {
        private static WindsorContainer container;

        [STAThread]
        public static void Main()
        {
            container = new WindsorContainer();
            ConfigureContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = container.Resolve<GraphicalVisualizer>();
            Application.Run(mainForm);
        }

        public static Bitmap GetTagCloudFromFile(string filepath)
        {
            var textReader = container.Resolve<ITextReader>();
            var preprocessor = container.Resolve<InputPreprocessor>();
            var statCounter = container.Resolve<StatisticsCounter>();
            var layouter = container.Resolve<WordLayouter>();
            var painter = container.Resolve<WordPainter>();
            var drawer = container.Resolve<WordDrawer>();
            var words = preprocessor.PreprocessWords(textReader, filepath);
            var analyzedText = statCounter.GetAnalyzedText(words);
            var analyzedLayoutedText = layouter.GetLayoutedText(analyzedText);
            var paintedWords = painter.GetPaintedWords(analyzedLayoutedText);
            return drawer.GetDrawnLayoutedWords(paintedWords);
        }

        private static void ConfigureContainer()
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));


            container.Register(Component.For<ITextReader>().ImplementedBy<TxtFileReader>().LifestyleTransient());


            container.Register(Component.For<IPreprocessor>().ImplementedBy<ToLowercasePreprocessor>());
            container.Register(Component.For<IPreprocessor>().ImplementedBy<RemoveNotWordsPreprocessor>());


            container.Register(Component.For<IStatisticsCollector>().ImplementedBy<WordCountCollector>());


            container.Register(Component.For<IWordSizeChooser>().ImplementedBy<WordCountSizeChooser>());


            container.Register(Component.For<WordPainter>()
                .ImplementedBy<DefaultWordPainter>()
                .LifestyleTransient());


            container.Register(Component.For<WordDrawer>().ImplementedBy<DefaultWordDrawer>());


            container.Register(Component.For<ICloudLayouter>()
                .UsingFactoryMethod(() =>
                {
                    var imageSettings = container.Resolve<ImageSettings>();
                    var imageCenter = new Point(imageSettings.Width / 2, imageSettings.Height / 2);
                    return new CircularCloudLayouter(imageCenter);
                })
                .LifestyleTransient());


            container.Register(Component.For<IGuiAction>().ImplementedBy<ImageSettingsAction>());
            container.Register(Component.For<IGuiAction>().ImplementedBy<FontSettingsAction>());
            container.Register(Component.For<IGuiAction>().ImplementedBy<PaletteSettingsAction>());
            container.Register(Component.For<IGuiAction>().ImplementedBy<TextFileAction>());
            container.Register(Component.For<IGuiAction>().ImplementedBy<SaveImageAction>());


            container.Register(Component.For<ImageSettings>().ImplementedBy<ImageSettings>().LifestyleSingleton());
            container.Register(Component.For<Font>().Instance(new Font(FontFamily.GenericSansSerif, 2)).LifestyleSingleton());
            container.Register(Component.For<Palette>().ImplementedBy<Palette>().LifestyleSingleton());
            container.Register(Component.For<PictureBoxImageHolder>().ImplementedBy<PictureBoxImageHolder>().LifestyleSingleton());
            container.Register(Component.For<AppSettings>().ImplementedBy<AppSettings>().LifestyleSingleton());


            container.Register(Classes.FromThisAssembly().Where(x => x.IsClass).LifestyleTransient());
        }
    }
}