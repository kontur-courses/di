using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.Layouter;
using TagsCloud.Layouter.Factory;
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
            return new ServiceCollection()
                .Scan(scan => scan.FromCallingAssembly().AddClasses().AsSelfWithInterfaces())

                .AddSingleton<IImageConfig, ImageConfig>()
                .AddSingleton<IWordsConfig, WordConfig>()

                .AddSingleton<ILayouterFactory, LayouterFactory>(s =>
                RegisterLayouterFactory(new LayouterFactory(s.GetService<IWordsConfig>())))


                .AddScoped<Form, ConfigWindow>()

                .BuildServiceProvider();
        }

        private static LayouterFactory RegisterLayouterFactory(LayouterFactory factory)
        {
            factory.Register("По спирали", center => new CircularCloudLayouter(center));
            return factory;
        }
    }
}
