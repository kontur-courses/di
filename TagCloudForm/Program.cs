using System;
using System.Windows.Forms;
using Autofac;
using TagCloud;
using TagCloud.CloudLayouter;
using TagCloud.Visualization;
using TagCloudForm.Actions;
using TagCloudForm.Holder;
using TagCloudForm.Settings;

namespace TagCloudForm
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();
            builder.RegisterType<CloudPainter>().AsSelf().SingleInstance();
            builder.RegisterType<TagCloudForm>().AsSelf();
            builder.RegisterType<SaveImageAction>().As<IUiAction>();
            builder.RegisterType<SpiralParametersAction>().As<IUiAction>();
            builder.RegisterType<AppSettings>().As<IImageDirectoryProvider, IImageSettingsProvider>().SingleInstance();
            builder.RegisterType<ImageSettings>().AsSelf();
            builder.RegisterType<PictureBoxImageHolder>().As<IImageHolder, PictureBoxImageHolder>().SingleInstance();
            builder.RegisterType<RectangleSettings>().AsSelf();
            builder.RegisterType<CircularCloudLayouter>().AsSelf().SingleInstance();
            builder.RegisterType<VisualizationSettings>().AsSelf();
            builder.RegisterType<TextPreparer>().AsSelf();
            builder.RegisterType<SpiralSettings>().AsSelf().SingleInstance();
            builder.RegisterType<ArchimedeanSpiral>().AsSelf().SingleInstance();
            builder.RegisterType<TextPreparerSettings>().AsSelf();

            var container = builder.Build();
            Application.Run(container.Resolve<TagCloudForm>());
        }
    }
}