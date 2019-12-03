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
            builder.RegisterType<DefaultExtracter>().SingleInstance().As<IExtracter>();
            builder.RegisterType<ArchimedeanSpiralLayouter>().As<ILayouter>();
            builder.RegisterType<DefaultParser>().SingleInstance().As<IParser>();
            builder.RegisterType<DefaultTxtReader>().SingleInstance().As<IReader>();
            builder.RegisterType<DefaultVisualizer>().SingleInstance().As<IVisualizer>();
            builder.Register(context => ImageSettings.GetDefaultSettings()).SingleInstance().As<ImageSettings>();
            builder.Register(context => FontSettings.GetDefaultSettings()).SingleInstance().As<FontSettings>();
            builder.Register(context => LayouterSettings.GetDefaultSettings()).SingleInstance().As<LayouterSettings>();
            builder.RegisterType<MainForm>().SingleInstance().As<MainForm>();
            return builder.Build();
        }

    }
}
