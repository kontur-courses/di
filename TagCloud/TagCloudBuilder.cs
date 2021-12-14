using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.configurations;
using TagCloud.file_readers;
using TagCloud.layouter;
using TagCloud.repositories;
using TagCloud.selectors;
using TagCloud.visual;

namespace TagCloud
{
    public class TagCloudBuilder
    {
        private readonly ServiceCollection container;
        private ITagRepositoryConfiguration tagRepositoryConfiguration;
        private IImageConfiguration imageConfiguration;
        private IImageSaveConfiguration imageSaveConfiguration;
        private string? filePath;

        private TagCloudBuilder()
        {
            tagRepositoryConfiguration = new TagRepositoryRepositoryConfiguration(
                Color.Magenta,
                FontFamily.GenericSansSerif,
                15
            );
            imageConfiguration = new ImageConfiguration(Color.Black, 1500, 1500);
            imageSaveConfiguration = new ImageSaveConfiguration("serious_out.png", ImageFormat.Png);
            container = new ServiceCollection();
            container
                .AddSingleton<IFileReader>(new TxtReader())
                .AddSingleton(new List<IChecker<string>> { new BoringChecker() })
                .AddSingleton<IFilter<string>, WordFilter>()
                .AddSingleton<IConverter<string>, ToLowerCaseConverter>()
                .AddSingleton<List<IConverter<string>>>()
                .AddSingleton<IConverter<IEnumerable<string>>, WordConverter>()
                .AddSingleton<IWordHelper, WordHelper>()
                .AddSingleton<IRepository<string>, WordRepository>()
                .AddSingleton<ICloudLayouter, CircularCloudLayouter>()
                .AddSingleton<IRepository<Tag>, TagRepository>()
                .AddSingleton<IVisualizer, TagVisualizer>()
                .AddSingleton<ISaver<Image>, TagVisualizationSaver>();
        }

        public static TagCloudBuilder Create() => new TagCloudBuilder();

        public TagCloudBuilder SetImageConfiguration(IImageConfiguration configuration)
        {
            imageConfiguration = configuration;
            return this;
        }

        public TagCloudBuilder SetImageSaveConfiguration(IImageSaveConfiguration configuration)
        {
            imageSaveConfiguration = configuration;
            return this;
        }

        public TagCloudBuilder SetTagRepositoryConfiguration(ITagRepositoryConfiguration configuration)
        {
            tagRepositoryConfiguration = configuration;
            return this;
        }

        public TagCloudBuilder SetInputFilePath(string? path)
        {
            filePath = path;
            return this;
        }

        public TagCloudBuilder Build()
        {
            if (filePath is null)
                throw new ArgumentException("Set filepath!");
            container
                .AddSingleton(filePath)
                .AddSingleton(tagRepositoryConfiguration)
                .AddSingleton(imageSaveConfiguration);
            return this;
        }

        public TagCloudBuilder Run()
        {
            var scope = container.BuildServiceProvider().CreateScope();
            var visualizer = scope.ServiceProvider.GetRequiredService<IVisualizer>();
            var saver = scope.ServiceProvider.GetRequiredService<ISaver<Image>>();
            saver.Save(visualizer.GetImage(imageConfiguration));
            return this;
        }
    }
}