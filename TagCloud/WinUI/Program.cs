using System;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualisation.Layouting;
using TagsCloudVisualisation.Output;

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
            return builder.Build();
        }
    }
}