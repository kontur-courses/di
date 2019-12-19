using Autofac;
using TagsCloudVisualization.ImageSaver;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Providers;
using TagsCloudVisualization.Providers.Layouter;
using TagsCloudVisualization.Providers.Layouter.Interfaces;
using TagsCloudVisualization.Providers.Layouter.Spirals;
using TagsCloudVisualization.Providers.Sizable;
using TagsCloudVisualization.Providers.Sizable.Interfaces;
using TagsCloudVisualization.WordSource;
using TagsCloudVisualization.WordSource.Changers;
using TagsCloudVisualization.WordSource.Interfaces;
using TagsCloudVisualization.WordSource.Readers;
using TagsCloudVisualization.WordSource.Selectors;

namespace TagsCloudVisualization
{
    internal class ContainerProvider
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TextReaderFactory>().AsSelf().SingleInstance();
            builder.RegisterType<TextSplitter>().As<IWordReader>().SingleInstance();
            builder.RegisterType<WordChangerFactory>().As<IChangerFactory>().SingleInstance();
            builder.RegisterType<WordSelectorFactory>().As<ISelectorFactory>().SingleInstance();
            builder.RegisterType<WordSourceProvider>().As<IWordsProvider>().SingleInstance();

            builder.RegisterType<WordFrequencyProvider>().As<IFrequencyProvider>();

            builder.RegisterType<SizeSelectorFactory>().AsSelf().SingleInstance();
            builder.RegisterType<SizableProvider>().As<ISizableProvider>().SingleInstance();

            builder.RegisterType<SpiralFactory>().AsSelf().SingleInstance();
            builder.RegisterType<DrawableCloudLayouter>().As<IDrawableProvider>();

            builder.RegisterType<TagDrawer>().As<IDrawer>().SingleInstance();

            builder.RegisterType<TagCreator>().AsSelf().SingleInstance();

            builder.RegisterType<ImageSaverFactory>().As<IImageSaverFactory>().SingleInstance();

            return builder.Build();
        }
    }
}