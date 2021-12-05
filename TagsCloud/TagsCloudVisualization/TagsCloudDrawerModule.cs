using System;
using Autofac;
using TagsCloudDrawer.Drawer;
using TagsCloudDrawer.ImageCreator;
using TagsCloudDrawer.ImageSavior;
using TagsCloudDrawer.ImageSettings;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.DrawableFactory;
using TagsCloudVisualization.DrawerSettingsProvider;
using TagsCloudVisualization.WordsPreprocessor;
using TagsCloudVisualization.WordsProvider;
using TagsCloudVisualization.WordsToTagsTransformers;

namespace TagsCloudVisualization
{
    public class TagsCloudDrawerModule : Module
    {
        private readonly TagsCloudDrawerModuleSettings _settings;

        public TagsCloudDrawerModule(TagsCloudDrawerModuleSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterInstance(new WordsFromFileProvider(_settings.WordsFile)).As<IWordsProvider>();
            builder.RegisterType<ToLowerCasePreprocessor>().As<IWordsPreprocessor>();
            builder.RegisterInstance(new RemoveBoredPreprocessor(_settings.BoredWords)).As<IWordsPreprocessor>();
            builder.RegisterComposite<IWordsPreprocessor>((_, processors) => new CombinedPreprocessor(processors));
            builder.RegisterInstance(_settings.ImageSettingsProvider).As<IImageSettingsProvider>();
            builder.RegisterInstance(_settings.TagDrawableSettingsProvider).As<ITagDrawableSettingsProvider>();
            builder.RegisterType<Drawer>().As<IDrawer>();
            builder.Register(_ => _settings.ImageSavior()).As<IImageSavior>();
            builder.Register(_ => _settings.Layouter).As<ILayouter>();
            builder.RegisterType<ImageCreator>().As<IImageCreator>();
            builder.RegisterType<LayoutWordsTransformer>().As<IWordsToTagsTransformer>();
            builder.RegisterType<TagDrawableFactory>().As<ITagDrawableFactory>();
            builder.RegisterType<TagsCloudVisualizer>().AsSelf();
        }
    }
}