using Autofac;
using TagsCloudContainer.ResultRenderer;
using TagsCloudContainer.WordFormatters;
using TagsCloudContainer.WordLayouts;
using TagsCloudContainer.WordsPreprocessors;
using TagsCloudContainer.WordsReaders;

namespace TagsCloudContainer
{
    public class CloudContainerBuilder
    {
        public IContainer BuildTagsCloudContainer(
            Config config,
            CircularCloudLayoutConfig circularCloudLayoutConfig)
        {
            var builder = new ContainerBuilder();

            builder.Register(z => new ImageRenderer(config.ImageSize))
                .As<IResultRenderer>();

            builder.Register(z => new CustomBoringWordsRemover(config.BoringWords))
                .As<IWordsPreprocessor>();

            builder.Register(z => new SimpleFormatter(
                    z.Resolve<WordsWeighter>(),
                    config.Font,
                    config.Color))
                .As<IWordFormatter>();

            builder.Register(z => new CircularCloudLayouter(circularCloudLayoutConfig))
                .As<ILayouter>();

            builder.RegisterTypes(typeof(BoringWordsRemover), typeof(WordsLower))
                .As<IWordsPreprocessor>();

            builder.RegisterType<WordsWeighter>()
                .AsSelf();

            builder
                .RegisterTypes(typeof(CustomBoringWordsRemover), typeof(WordsLower), typeof(BoringWordsRemover))
                .As<IWordsPreprocessor>();

            builder
                .RegisterType<TagsCloudBuilder>()
                .AsSelf();

            return builder.Build();
        }

        public IContainer BuildReaderContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TxtReader>()
                .As<IWordsReader>();

            return builder.Build();
        }
    }
}