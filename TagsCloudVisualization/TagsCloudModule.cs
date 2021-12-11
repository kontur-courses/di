using System.Collections.Generic;
using Autofac;
using TagsCloudVisualization.ColorService;
using TagsCloudVisualization.DrawableContainers.Builders;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.FontService;
using TagsCloudVisualization.ImageCreators;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.PointGenerators;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.SizeService;
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
        private readonly GeneralSettings generalSettings;

        public TagsCloudModule(GeneralSettings settings) =>
            generalSettings = settings;

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterInstance(generalSettings).As<GeneralSettings>();
            builder.RegisterType<TxtFileReader>().As<IWordsReader>();
            
            builder.Register(ctx =>
            {
                var settings = ctx.ResolveSettings();
                var readers = ctx.Resolve<IEnumerable<IWordsReader>>();
                return new FileReadService(settings.Reader.FileWithWords, readers);
            }).As<IFileReadService>();
            
            builder.RegisterType<WordsToLowerPrepare>().As<IWordsPreparer>();
            
            builder.Register(ctx => 
            {
                var settings = ctx.ResolveSettings();
                return new BoringWordsFilter(settings.WordsPreprocessor.BoringWords);
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
                return new TagColorService(settings.Drawer.TagColor);
            }).As<ITagColorService>();
            
            builder.RegisterType<WordsToTagTransformer>().As<IWordsToTagTransformer>();
            builder.RegisterType<DrawableContainerBuilder>().As<IDrawableContainerBuilder>();
            
            builder.Register(ctx =>
            {
                var settings = ctx.ResolveSettings();
                return new ArchimedeanSpiralPointGenerator(settings.StartPoint);
            }).As<IPointGenerator>();
            
            builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();

            builder.Register(ctx =>
            {
                var settings = ctx.ResolveSettings();
                return new TagFontService(settings.Font.MaxSize, settings.Font.Family);
            }).As<ITagFontService>();
            
            
            builder.RegisterType<TagSizeService>().As<ITagSizeService>();

            builder.RegisterType<ImageCreator>().As<IImageCreator>();
            builder.RegisterType<Visualizer>().AsSelf();
        }
    }
}