using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.TextProcessing.WordConfig;
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

            Application.Run(BuildContainer().GetService<Form>());
        }

        public static ServiceProvider BuildContainer()
        {
            var services = new ServiceCollection();
            services.Scan(scan => scan
                                    .FromCallingAssembly()
                                    .AddClasses()
                                    .AsSelfWithInterfaces());

            services
                .AddSingleton<IImageConfig, ImageConfig>()
                .AddSingleton<IWordsConfig, WordConfig>()
                .AddScoped<Form, ConfigWindow>();

            return services.BuildServiceProvider();
        }
    }
}
