using Autofac;
using TagCloudContainer;
using TagCloudContainer.Filters;
using TagCloudContainer.Formatters;
using TagCloudContainer.FrequencyWords;
using TagCloudContainer.Parsers;
using TagCloudContainer.PointAlgorithm;
using TagCloudContainer.Readers;
using TagCloudContainer.Rectangles;
using TagCloudContainer.TagsWithFont;
using TagCloudGraphicalUserInterface.Actions;
using TagCloudGraphicalUserInterface.Interfaces;
using TagCloudGraphicalUserInterface.Settings;

namespace TagCloudGraphicalUserInterface
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var builder = new ContainerBuilder();
            builder.RegisterType<PresetsSettings>().As<IPresetsSettings, PresetsSettings>().SingleInstance();
            builder.RegisterType<FontSettings>().As<IFontSettings>();

            builder.RegisterType<FilterWords>().As<IFilter>().SingleInstance();
            builder.RegisterType<WordFormatter>().As<IWordFormatter>().SingleInstance();
            builder.RegisterType<FileLinesParser>().As<IFileParser>().SingleInstance();
            builder.RegisterType<Reader>().As<IFileReader>().SingleInstance();
            builder.RegisterType<FontSizer>().As<IFontSizer>().SingleInstance();
            builder.RegisterType<FrequencyTags>().As<IFrequencyCounter>().SingleInstance();
            builder.RegisterType<TagCloudDrawer>().As<ICloudDrawer>().SingleInstance();

            builder.RegisterType<SaveAction>().As<IActionForm>();
            builder.RegisterType<FontAction>().As<IActionForm>();
            builder.RegisterType<TagAction>().As<IActionForm>();
            builder.RegisterType<PresetAction>().As<IActionForm>();
            builder.RegisterType<SourceTagsAction>().As<IActionForm>();
            builder.RegisterType<PaletteAction>().As<IActionForm>();

            builder.RegisterType<ArithmeticSpiral>().As<IPointer>().InstancePerLifetimeScope();
            builder.RegisterType<FontSettings>().As<IFontSettings>();
            builder.RegisterType<RectangleBuilder>().As<IRectangleBuilder>();
            builder.RegisterType<CloudCreateSettings>().As<ICloudCreateSettings>();
            builder.RegisterInstance(new PointConfig(1, 1)).As<IPointConfig>();
            builder.RegisterType<Palette>().AsSelf().SingleInstance();
            builder.RegisterType<PictureBoxTags>().As<IImageSettingsProvider, PictureBoxTags>().SingleInstance();
            builder.RegisterType<AlgorithmSettings>().As<IAlgorithmSettings>().SingleInstance();

            builder.RegisterTypes(typeof(TagCloud), typeof(TextRectangle), typeof(CloudForm), typeof(ImageSettings)).AsSelf();
            var container = builder.Build();
            Application.Run(container.Resolve<CloudForm>());

        }
    }
}
