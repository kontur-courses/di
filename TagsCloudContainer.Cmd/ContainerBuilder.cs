using System.Drawing.Imaging;
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
        public TagsCloudBuilder BuildTagsCloudContainer(
            Config config,
            CircularCloudLayoutConfig circularCloudLayoutConfig)
        {
            var builder = new Autofac.ContainerBuilder();

            builder
                .RegisterType<ImageRenderer>()
                .As<IResultRenderer>()
                .WithParameter("imageSize", config.ImageSize)
                .WithParameter("imageFormat", ImageFormat.Png);

            builder
                .RegisterType<SimpleFormatter>()
                .As<IWordFormatter>()
                .WithParameter("font", config.Font)
                .WithParameter("color", config.Color);

            builder.RegisterTypes(typeof(CustomBoringWordsRemover), typeof(WordsLower))
                .As<IWordsPreprocessor>();

            builder.RegisterType<CircularCloudLayouter>()
                .As<ILayouter>()
                .WithParameter("config", circularCloudLayoutConfig);

            builder.RegisterType<TxtReader>()
                .As<IWordsReader>();

            builder
                .RegisterTypes(typeof(CustomBoringWordsRemover), typeof(WordsLower), typeof(BoringWordsRemover))
                .As<IWordsPreprocessor>();

            builder
                .RegisterType<TagsCloudBuilder>()
                .AsSelf();

            return builder.Build()
                .Resolve<TagsCloudBuilder>();
        }
    }
}