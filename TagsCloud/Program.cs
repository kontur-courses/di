using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using TagsCloud.ImageProcessing;
using TagsCloud.ImageProcessing.Config.ImageBuilders;
using TagsCloud.ImageProcessing.ImageBuilders;
using TagsCloud.ImageProcessing.SaverImage;
using TagsCloud.Layouter;
using TagsCloud.TextProcessing;
using TagsCloud.TextProcessing.Converters;
using TagsCloud.TextProcessing.TextFilters;
using TagsCloud.TextProcessing.TextReaders;
using TagsCloud.TextProcessing.Word;
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

            var services = new ServiceCollection();
            services
                .AddSingleton<IImageConfig, ImageConfig>()
                .AddSingleton<IWordsConfig, WordConfig>()
                .AddScoped<ITextFilter, FunctionWordsFilter>()
                .AddScoped<ITextReader, TextReader>()
                .AddScoped<IWordConverter, LowerCaseConverter>()
                .AddScoped<ILayouter, CircularCloudLayouter>()
                .AddScoped<TextOperator, TextOperator>()
                .AddScoped<IImageSaver, ImageSaver>()
                .AddScoped<IImageBuilder, ImageBuilder>()
                .AddScoped<Form, ConfigWindow>();

            var provider = services.BuildServiceProvider();

            var window = provider.GetService<Form>();
            Application.Run(window);
        }
    }
}
