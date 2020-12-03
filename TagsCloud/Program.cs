using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using TagsCloud.Extensions;
using TagsCloud.UserInterfaces.GUI;

namespace TagsCloud
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(BuildContainer().ConfigureFactories().GetService<Form>());
        }

        private static ServiceProvider BuildContainer()
        {
            return new ServiceCollection()
                .Scan(scan => scan.FromCallingAssembly().AddClasses().AsSelfWithInterfaces().WithSingletonLifetime())

                .AddSingleton<Form, ConfigWindow>()

                .BuildServiceProvider();
        }
    }
}
