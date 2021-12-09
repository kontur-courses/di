using System;
using System.Collections.Generic;
using Autofac;
using TagsCloudVisualization.ColorService;
using TagsCloudVisualization.FontService;
using TagsCloudVisualization.ImageCreator;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.PointGenerators;
using TagsCloudVisualization.Saver;
using TagsCloudVisualization.SizeService;
using TagsCloudVisualization.TagToDrawableTransformer;
using TagsCloudVisualization.WordsPrepare;
using TagsCloudVisualization.WordsPrepare.Preparers;
using TagsCloudVisualization.WordsProvider;
using TagsCloudVisualization.WordsProvider.FileReader;
using TagsCloudVisualization.WordsToTagTransformer;

namespace TagsCloudVisualization
{
    public class TagsCloudDrawerModule : Module
    {
        private readonly Settings settings;

        public TagsCloudDrawerModule(Settings settings) =>
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<TxtFileReader>().As<IWordsReader>();
            builder.Register(ctx => new FileReadService(settings.FileWithWords,
                    ctx.Resolve<IEnumerable<IWordsReader>>()))
                .As<IFileReadService>();
            builder.RegisterType<WordsToLowerPrepare>().As<IWordsPreparer>();
            builder.RegisterInstance(new BoringWordsFilter(settings.BoringWords)).As<IWordsPreparer>();
            builder.RegisterComposite<IWordsPreparer>((_, processors) => new WordsPreparer(processors));
            builder.RegisterInstance(new TagColorService(settings.TagColor)).As<ITagColorService>();
            builder.RegisterType<WordsToTagTransformer.WordsToTagTransformer>().As<IWordsToTagTransformer>();
            builder.Register(_ => new ArchimedeanSpiralPointGenerator(settings.StartPoint)).As<IPointGenerator>();
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
            builder.Register(_ => new TagFontService(settings.MaxFontSize, settings.FontFamilyName))
                .As<ITagFontService>();
            builder.RegisterType<TagSizeService>().As<ITagSizeService>();
            builder.RegisterType<TagToDrawableTagTransformer>().As<ITagToDrawableTransformer>();
            builder.RegisterInstance(new ImageSaver(settings.Directory, settings.ImageName)).As<IImageSaver>();
            builder.RegisterType<ImageCreator.ImageCreator>().As<IImageCreator>();
            builder.RegisterType<Visualizer>().AsSelf();
        }
    }
}