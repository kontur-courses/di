using Autofac;
using TagsCloudContainer.ResultRenderer;
using TagsCloudContainer.WordFormatters;
using TagsCloudContainer.WordLayouts;
using TagsCloudContainer.WordsPreprocessors;
using TagsCloudContainer.WordsReaders;

namespace TagsCloudContainer.Cmd
{
    public class ContainerBuilder
    {
        public IContainer BuildTagsCloudContainer(
            Config config,
            CircularCloudLayoutConfig circularCloudLayoutConfig)
        {
            var builder = new Autofac.ContainerBuilder();

            builder.Register(z => new ImageRenderer(config.ImageSize))
                .As<IResultRenderer>();

            builder.Register(z => new SimpleFormatter(config.Font, config.Color))
                .As<IWordFormatter>();

            builder.Register(z => new CircularCloudLayouter(circularCloudLayoutConfig))
                .As<ILayouter>();

            builder.RegisterTypes(typeof(CustomBoringWordsRemover), typeof(WordsLower))
                .As<IWordsPreprocessor>();

            builder.RegisterType<TxtReader>()
                .AsSelf();

            builder
                .RegisterTypes(typeof(CustomBoringWordsRemover), typeof(WordsLower), typeof(BoringWordsRemover))
                .As<IWordsPreprocessor>();

            builder
                .RegisterType<TagsCloudBuilder>()
                .AsSelf();

            return builder.Build();
        }
    }
}