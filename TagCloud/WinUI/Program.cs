using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualisation.Configuration;
using TagsCloudVisualisation.Layouting;
using TagsCloudVisualisation.Output;
using TagsCloudVisualisation.Text;

namespace WinUI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = InitContainer();
            Application.Run(container.Resolve<MainForm>());
        }

        private static IContainer InitContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(ITagCloudLayouter).Assembly, typeof(Program).Assembly)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterGeneric(typeof(InputRequester<>))
                .As(typeof(IConfigEntry<>));

            builder.RegisterType<WordsFileReader>()
                .As<IWordsReader>()
                .WithParameter("delimiters", new [] {'\n', ' '});

            builder.RegisterType<CircularTagCloudLayouter>()
                .As<ITagCloudLayouter>()
                .WithParameter("cloudCenter", new Point())
                .WithParameter("minRectangleSize", new Size(3, 3));

            builder.RegisterType<FileResultWriter>()
                .As<IResultWriter>()
                .WithParameter("format", ImageFormat.Png);

            return builder.Build();
        }
    }
}