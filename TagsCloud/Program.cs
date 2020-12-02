using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using TagsCloud.Extensions;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.ImageProcessing.SaverImage.Factory;
using TagsCloud.Layouter.Factory;
using TagsCloud.TagsCloudProcessing.TagsGeneratorFactory;
using TagsCloud.TextProcessing.Converters;
using TagsCloud.TextProcessing.TextFilters;
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

            Application.Run(BuildContainer().ConfigureFactories().GetService<Form>());
        }

        private static ServiceProvider BuildContainer()
        {
            return new ServiceCollection()
                .Scan(scan => scan.FromCallingAssembly().AddClasses().AsSelfWithInterfaces().WithTransientLifetime())

                .AddSingleton<IImageConfig, ImageConfig>()
                .AddSingleton<IWordsConfig, WordConfig>()

                .AddSingleton<IRectanglesLayoutersFactory, RectanglesLayoutersFactory>()
                .AddSingleton<ITagsGeneratorFactory, TagsGeneratorFactory>()
                .AddSingleton<IConvertersApplier, ConvertersApplier>()
                .AddSingleton<IImageSaverFactory, ImageSaverFactory>()
                .AddSingleton<IFiltersApplier, FiltersApplier>()

                .AddSingleton<Form, ConfigWindow>()

                .BuildServiceProvider();
        }
    }
}
