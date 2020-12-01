using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Autofac;
using Autofac.Core;
using TagsCloudVisualisation;
using TagsCloudVisualisation.Layouting;
using TagsCloudVisualisation.Output;
using TagsCloudVisualisation.Text;
using TagsCloudVisualisation.Visualisation;

namespace WinUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = InitContainer();
            container.Resolve<App>().Subscribe();
            
            var form = container.Resolve<MainForm>();
            Application.Run(form);
        }

        private static IContainer InitContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FileWordsReader>()
                .AsImplementedInterfaces()
                .AsSelf()
                .WithParameter("delimiters", new [] {',', '.', ' ', '!', '?', '\n', '\r', '\t', '&', '#', '-'});

            builder.RegisterType<CircularTagCloudLayouter>()
                .AsImplementedInterfaces()
                .AsSelf()
                .WithParameter("cloudCenter", new Point())
                .WithParameter("minRectangleSize", new Size(3, 3));

            builder.RegisterType<FileResultWriter>()
                .AsImplementedInterfaces()
                .AsSelf()
                .WithParameter("format", ImageFormat.Png);

            builder.RegisterType<CloudVisualiser>()
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterType<TagCloudGenerator>()
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(ITagCloudLayouter).Assembly, typeof(Program).Assembly)
                .Where(t => !builder.ComponentRegistryBuilder.IsRegistered(new TypedService(t)))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();

            return builder.Build();
        }
    }
}