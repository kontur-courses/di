using Autofac;
using System.Windows.Forms;

namespace TagCloud
{
    public static class Program
    {
        public static void Main()
        {
            var container = BuildContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = container.Resolve<MainForm>();
            Application.Run(mainForm);
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DefaultExtractor>().SingleInstance().As<IExtractor>();
            builder.RegisterType<DefaultFilter>().SingleInstance().As<IFilter>();
            builder.RegisterType<ArchimedeanSpiralLayouter>().As<ILayouter>();
            builder.RegisterType<DefaultParser>().SingleInstance().As<IParser>();
            builder.RegisterType<DefaultTxtReader>().SingleInstance().As<IReader>();
            builder.RegisterType<DefaultVisualizer>().SingleInstance().As<IVisualizer>();
            builder.Register(context => FileSettings.GetDefaultSettings()).SingleInstance().As<FileSettings>();
            builder.Register(context => FilterSettings.GetDefaultSettings()).SingleInstance().As<FilterSettings>();
            builder.Register(context => ImageSettings.GetDefaultSettings()).SingleInstance().As<ImageSettings>();
            builder.Register(context => FontSettings.GetDefaultSettings()).SingleInstance().As<FontSettings>();
            builder.Register(context => LayouterSettings.GetDefaultSettings()).SingleInstance().As<LayouterSettings>();
            builder.RegisterType<MainForm>().SingleInstance().As<MainForm>();
            return builder.Build();
        }

    }
}
