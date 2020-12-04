using System;
using System.Windows.Forms;
using Autofac;
using Autofac.Core;
using TagsCloudVisualisation.Layouting;

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
            using (var lifetimeScope = container.BeginLifetimeScope())
            {
                lifetimeScope.Resolve<App>().Subscribe();

                var form = lifetimeScope.Resolve<MainForm>();
                Application.Run(form);
            }
        }

        private static IContainer InitContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(ITagCloudLayouter).Assembly, typeof(Program).Assembly)
                .Where(t => !builder.ComponentRegistryBuilder.IsRegistered(new TypedService(t)))
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance()
                .OwnedByLifetimeScope();

            return builder.Build();
        }
    }
}