using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.Layouter;
using TagsCloud.Layouter.Factory;
using TagsCloud.TagsCloudProcessing.TagsGeneratorFactory;
using TagsCloud.TagsCloudProcessing.TegsGenerators;
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

            var container = BuildContainer();
            InitFactories(container);

            Application.Run(container.GetService<Form>());
        }

        public static ServiceProvider BuildContainer()
        {
            return new ServiceCollection()
                .Scan(scan => scan.FromCallingAssembly().AddClasses().AsSelfWithInterfaces())

                .AddSingleton<IImageConfig, ImageConfig>()
                .AddSingleton<IWordsConfig, WordConfig>()

                .AddSingleton<ILayouterFactory, LayouterFactory>()
                .AddSingleton<ITagsGeneratorFactory, TagsGeneratorFactory>()
                .AddSingleton<IConvertersApplier, ConvertersApplier>()
                .AddSingleton<IFiltersApplier, FiltersApplier>()

                .AddScoped<Form, ConfigWindow>()

                .BuildServiceProvider();
        }

        private static void InitFactories(ServiceProvider serviceProvider)
        {
            serviceProvider.GetService<ILayouterFactory>()
                .Register("По спирали", center => new CircularCloudLayouter(center));

            serviceProvider.GetService<ITagsGeneratorFactory>()
                .Register("Топ 30", () => new TagsGenerator());

            serviceProvider.GetService<IConvertersApplier>()
                .Register("Перевести в нижний регистр", new LowerCaseConverter());

            serviceProvider.GetService<IFiltersApplier>()
                .Register("Исключить служебные символы", new FunctionWordsFilter());
        }
    }
}
