using Autofac;
using TagCloudContainer.PointAlgorithm;
using TagCloudContainer.Rectangles;
using TagsCloudVisualization;

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
            builder.RegisterType<TagCloudSettings>().As<TagCloudSettings>();
            builder.RegisterType<TagAction>().As<IActionForm>();
            builder.RegisterType<PaletteAction>().As<IActionForm>();
            builder.RegisterType<Palette>().AsSelf().SingleInstance();
            builder.RegisterType<PictureBoxTags>().As<IImageSettingsProvider, PictureBoxTags>().SingleInstance();
            builder.RegisterTypes(typeof(CircularCloudLayouter), typeof(ArithmeticSpiral), typeof(TagCloud),
                typeof(TextRectangle),
                typeof(AppSettings), typeof(CloudForm), typeof(ImageSettings)).AsSelf();
            var container = builder.Build();
            Application.Run(container.Resolve<CloudForm>());

        }
    }
}
