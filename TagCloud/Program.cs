using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TagCloud.file_readers;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.configurations;
using TagCloud.layouter;
using TagCloud.repositories;
using TagCloud.selectors;
using TagCloud.visual;

namespace TagCloud
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var defaultTagRepositoryConfiguration =
                new TagRepositoryRepositoryConfiguration(Color.Magenta, FontFamily.GenericSansSerif, 15);
            var defaultImageConfiguration = new ImageConfiguration(Color.Black, 1500, 1500);
            var defaultImageSaveConfiguration = new ImageSaveConfiguration("serious_out.png", ImageFormat.Png);

            var services = new ServiceCollection()
                .AddSingleton("simple_input.txt")
                .AddSingleton<IFileReader>(new TxtReader())
                .AddSingleton<List<IChecker<string>>>()
                .AddSingleton<IFilter<string>, WordFilter>()
                .AddSingleton<IConverter<string>, ToLowerCaseConverter>()
                .AddSingleton<List<IConverter<string>>>()
                .AddSingleton<IConverter<IEnumerable<string>>, WordConverter>()
                .AddSingleton<IWordHelper, WordHelper>()
                .AddSingleton<IRepository<string>, WordRepository>()
                .AddSingleton<ITagRepositoryConfiguration>(defaultTagRepositoryConfiguration)
                .AddSingleton<ICloudLayouter, CircularCloudLayouter>()
                .AddSingleton<IRepository<Tag>, TagRepository>()
                .AddSingleton<IImageConfiguration>(defaultImageConfiguration)
                .AddSingleton<IVisualizer, TagVisualizer>()
                .AddSingleton<IImageSaveConfiguration>(defaultImageSaveConfiguration)
                .AddSingleton<ISaver<Image>, TagVisualizationSaver>();
            var container = services.BuildServiceProvider();
            var scope = container.CreateScope();
            var imageConfiguration = scope.ServiceProvider.GetRequiredService<IImageConfiguration>();
            var visualizer = scope.ServiceProvider.GetRequiredService<IVisualizer>();
            var saver = scope.ServiceProvider.GetRequiredService<ISaver<Image>>();
            saver.Save(visualizer.GetImage(imageConfiguration));
        }
    }
}