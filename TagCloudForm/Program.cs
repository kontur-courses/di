using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using TagCloud.CloudLayouter;
using TagCloud.TextFilter;
using TagCloud.TextProvider;
using TagCloud.Visualization;
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
            builder.RegisterType<TagCloudForm>().AsSelf().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Action"))
                .AsImplementedInterfaces();

            builder.RegisterType<AppSettings>().As<IImageDirectoryProvider>().SingleInstance();
            builder.RegisterType<AppSettings>().AsSelf().SingleInstance();
            builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
            builder.RegisterType<Size>().AsSelf();
            builder.RegisterType<PictureBoxImageHolder>().As<IImageHolder, PictureBoxImageHolder>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
            builder.RegisterType<ViewSettings>().AsSelf().SingleInstance();
            builder.RegisterType<BlacklistMaker>().AsSelf().SingleInstance();
            builder.RegisterType<TextFilter>().AsSelf();
            builder.RegisterType<SpiralSettings>().AsSelf().SingleInstance();
            builder.RegisterType<ArchimedeanSpiral>().AsSelf().SingleInstance();
            builder.RegisterType<BlacklistSettings>().AsSelf().SingleInstance();
            builder.RegisterType<TextFileReader>().As<ITextProvider>().SingleInstance();
            builder.RegisterType<FrequencyDictionaryMaker>().AsSelf().SingleInstance();

            var container = builder.Build();
            Application.Run(container.Resolve<TagCloudForm>());
        }
    }
}