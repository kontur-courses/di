using System;
using System.Windows.Forms;
using Autofac;

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

            //TODO modules
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance()
                .OwnedByLifetimeScope();

            return builder.Build();
        }
    }
}