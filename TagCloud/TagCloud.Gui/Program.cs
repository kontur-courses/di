using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autofac;

namespace TagCloud.Gui
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var lifetimeScope = InitContainer().BeginLifetimeScope())
            {
                lifetimeScope.Resolve<App>().Subscribe();
                lifetimeScope.Resolve<IUi>().Run();
            }
        }

        private static IContainer InitContainer()
        {
            var builder = new ContainerBuilder();

            var dlls = Directory.EnumerateFiles(Environment.CurrentDirectory, "TagCloud*.dll");
            var assemblies = dlls.Select(Assembly.LoadFrom).ToArray();
            builder.RegisterAssemblyModules(assemblies);

            return builder.Build();
        }
    }
}