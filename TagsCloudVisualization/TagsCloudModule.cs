using System;
using System.Collections.Generic;
using Autofac;
using TagsCloudVisualization.ColorService;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.FontService;
using TagsCloudVisualization.ImageCreators;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.PointGenerators;
using TagsCloudVisualization.Saver;
using TagsCloudVisualization.SizeService;
using TagsCloudVisualization.TagToDrawableTransformer;
using TagsCloudVisualization.WordsPreprocessors;
using TagsCloudVisualization.WordsPreprocessors.Filters;
using TagsCloudVisualization.WordsPreprocessors.Preparers;
using TagsCloudVisualization.WordsProvider;
using TagsCloudVisualization.WordsProvider.FileReader;
using TagsCloudVisualization.WordsToTagTransformers;

namespace TagsCloudVisualization
{
    public class TagsCloudModule : Module
    {
        private readonly Settings visualizationSettings;

        public TagsCloudModule(Settings settings) =>
            visualizationSettings = settings;

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterInstance(visualizationSettings).As<Settings>();
            builder.RegisterType<TxtFileReader>().As<IWordsReader>();
            
            builder.Register(ctx =>
            {
                var settings = ctx.ResolveSettings();
                var readers = ctx.Resolve<IEnumerable<IWordsReader>>();
                return new FileReadService(settings.FileWithWords, readers);
            }).As<IFileReadService>();
            
            builder.RegisterType<WordsToLowerPrepare>().As<IWordsPreparer>();
            
            builder.Register(ctx => 
            {
                var settings = ctx.ResolveSettings();
                return new BoringWordsFilter(settings.BoringWords);
            }).As<IWordsFilter>();
            
            builder.Register(ctx =>
            {
                var preparers = ctx.Resolve<IEnumerable<IWordsPreparer>>();
                var filters = ctx.Resolve<IEnumerable<IWordsFilter>>();
                return new WordsPreprocessor(preparers, filters);
            }).As<IWordsPreprocessor>();

            builder.Register( ctx =>
            {
                var settings = ctx.ResolveSettings();
                return new TagColorService(settings.TagColor);
            }).As<ITagColorService>();
            
            builder.RegisterType<WordsToTagTransformer>().As<IWordsToTagTransformer>();
            
            builder.Register(ctx =>
            {
                var settings = ctx.ResolveSettings();
                return new ArchimedeanSpiralPointGenerator(settings.StartPoint);
            }).As<IPointGenerator>();
            
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();

            builder.Register(ctx =>
            {
                var settings = ctx.ResolveSettings();
                return new TagFontService(settings.MaxFontSize, settings.FontFamilyName);
            }).As<ITagFontService>();
            
            
            builder.RegisterType<TagSizeService>().As<ITagSizeService>();
            builder.RegisterType<TagToDrawableTagTransformer>().As<ITagToDrawableTransformer>();
            
            builder.Register(ctx =>
            {
                var settings = ctx.ResolveSettings();
                return new ImageSaver(settings.Directory, settings.ImageName);
            }).As<IImageSaver>();

            builder.RegisterType<ImageCreator>().As<IImageCreator>();
            builder.RegisterType<Visualizer>().AsSelf();
        }
    }
}