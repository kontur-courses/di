using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
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
using TextReader = TagsCloudVisualization.Text.TextReader;

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
            container.Register(Component.For<Stream>().UsingFactoryMethod(() =>
            {
                var memoryStream = new MemoryStream();
                using (var fileStream = new FileStream(filepath, FileMode.Open))
                {
                    fileStream.CopyTo(memoryStream);  //TODO: Put this into new abstraction
                }
                memoryStream.Position = 0;
                return memoryStream;
            }).Named(Guid.NewGuid().ToString())
                .IsDefault());
            var layouter = container.Resolve<WordLayouter>();
            return container.Resolve<WordLayoutPainter>().GetDrawnLayoutedWords(layouter.GetLayoutedWords());
        }

        private static void ConfigureContainer()
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true)); //TODO: Register all base classes automatically

            container.Register(Component.For<TextReader>().ImplementedBy<TxtReader>().LifestyleTransient());

            container.Register(Component.For<IPreprocessAction>().ImplementedBy<ToLowercase>());
            //container.Register(Component.For<IPreprocessAction>().ImplementedBy<PreprocessAction>()); //TODO: Add part of speech tagger

            container.Register(Component
                .For<WordLayoutPainter>()
                .ImplementedBy<DefaultWordLayoutPainter>()
                .LifestyleTransient());

            container.Register(Component.For<ICloudLayouter>()
                .UsingFactoryMethod(() =>
                {
                    var imageSettings = container.Resolve<ImageSettings>();
                    var imageCenter = new Point(imageSettings.Width / 2, imageSettings.Height / 2);
                    return new CircularCloudLayouter(imageCenter);
                })
                .LifestyleTransient());

            container.Register(Component.For<Preprocessor>().ImplementedBy<Preprocessor>());

            container.Register(Component.For<IGuiAction>().ImplementedBy<ImageSettingsAction>());
            container.Register(Component.For<IGuiAction>().ImplementedBy<FontSettingsAction>());
            container.Register(Component.For<IGuiAction>().ImplementedBy<PaletteSettingsAction>());
            container.Register(Component.For<IGuiAction>().ImplementedBy<TextFileAction>());
            container.Register(Component.For<IGuiAction>().ImplementedBy<SaveImageAction>());

            container.Register(Component.For<WordLayouter>()
                .UsingFactoryMethod(() =>
                {
                    var textReader = container.Resolve<TextReader>();
                    var words = WordReader.GetAllWords(textReader);
                    var preprocessor = container.Resolve<Preprocessor>();
                    var cloudLayouter = container.Resolve<ICloudLayouter>();
                    return new WordLayouter(preprocessor.PreprocessWords(words), cloudLayouter);
                }).LifestyleTransient());

            container.Register(Component.For<ImageSettings>().ImplementedBy<ImageSettings>().LifestyleSingleton());
            container.Register(Component.For<Font>().Instance(new Font(FontFamily.GenericSansSerif, 2)).LifestyleSingleton());
            container.Register(Component.For<Palette>().ImplementedBy<Palette>().LifestyleSingleton());
            container.Register(Component.For<PictureBoxImageHolder>().ImplementedBy<PictureBoxImageHolder>().LifestyleSingleton());
            container.Register(Component.For<AppSettings>().ImplementedBy<AppSettings>().LifestyleSingleton());

            container.Register(Component.For<GraphicalVisualizer>().ImplementedBy<GraphicalVisualizer>());
        }
    }
}