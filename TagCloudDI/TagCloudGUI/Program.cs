using Autofac;
using TagCloudContainer;
using TagCloudContainer.BoringFilters;
using TagCloudContainer.Processors;
using TagCloudContainer.FrequencyWords;
using TagCloudContainer.Interfaces;
using TagCloudContainer.Models;
using TagCloudContainer.Parsers;
using TagCloudContainer.PointAlgorithm;
using TagCloudContainer.Readers;
using TagCloudContainer.Rectangles;
using TagCloudContainer.TagsWithFont;
using TagCloudGUI.Actions;
using TagCloudGUI.Interfaces;
using TagCloudGUI.Settings;
using TagCloudContainer.TagSorters;
using TagCloudContainer.FrequencySorters;

namespace TagCloudGUI
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
            builder.RegisterType<FontSettings>().As<IFontSettings>();

            builder.RegisterType<DefaultFrequencySorter>().As<IFrequencySorter>();
            builder.RegisterType<BoringFilter>().As<IBoringWordsFilter>().SingleInstance();
            builder.RegisterType<WordProcessor>().As<IWordProcessor>().SingleInstance();
            builder.RegisterType<FileLinesParser>().As<IFileParser>().SingleInstance();
            builder.RegisterType<Reader>().As<IFileReader>().SingleInstance();
            builder.RegisterType<FontSizer>().As<IFontSizer>().SingleInstance();
            builder.RegisterType<FrequencyCounter>().As<IFrequencyCounter>().SingleInstance();
            builder.RegisterType<TagCloudDrawer>().As<ICloudDrawer>().SingleInstance();

            builder.RegisterType<SaveAction>().As<IActionForm>();
            builder.RegisterType<TagAction>().As<IActionForm>();
            builder.RegisterType<PaletteAction>().As<IActionForm>();

            builder.RegisterType<ArithmeticSpiral>().As<IPointProvider>();
            builder.RegisterType<FontSettings>().As<IFontSettings>();
            builder.RegisterType<RectangleBuilder>().As<IRectangleBuilder>();
            builder.RegisterType<Palette>().AsSelf().SingleInstance();
            builder.RegisterType<PictureBoxTags>().As<IImageSettingsProvider, PictureBoxTags>().SingleInstance();
            builder.RegisterType<AlgorithmSettings>().As<IPresetsSettings, IAlgorithmSettings>().SingleInstance();

            builder.RegisterType<TagCloud>().As<ITagCloud>();
            builder.RegisterTypes(typeof(RectangleWithText), typeof(CloudForm), typeof(ImageSettings)).AsSelf();
            
            
            var container = builder.Build();
            Application.Run(container.Resolve<CloudForm>());

        }
    }
}
