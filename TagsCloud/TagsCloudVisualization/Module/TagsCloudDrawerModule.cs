using System;
using System.IO;
using Autofac;
using TagsCloudDrawer.Drawer;
using TagsCloudDrawer.ImageCreator;
using TagsCloudDrawer.ImageSaveService;
using TagsCloudDrawer.ImageSettings;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.DrawableFactory;
using TagsCloudVisualization.DrawerSettingsProvider;
using TagsCloudVisualization.WordsPreprocessor;
using TagsCloudVisualization.WordsProvider;
using TagsCloudVisualization.WordsToTagsTransformers;

namespace TagsCloudVisualization.Module
{
    public class TagsCloudDrawerModule : Autofac.Module
    {
        private readonly TagsCloudDrawerModuleSettings _settings;

        public TagsCloudDrawerModule(TagsCloudDrawerModuleSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterInstance(GetWordsFromFileProviderForFile(_settings.WordsFile)).As<IWordsProvider>();
            RegisterWordPreprocessors(builder);
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

        private void RegisterWordPreprocessors(ContainerBuilder builder)
        {
            builder.RegisterType<ToLowerCasePreprocessor>().As<IWordsPreprocessor>();
            foreach (var preprocessor in _settings.WordsPreprocessors)
                builder.RegisterInstance(preprocessor).As<IWordsPreprocessor>();
            builder.RegisterInstance(new RemoveBoredPreprocessor(_settings.BoredWords)).As<IWordsPreprocessor>();
            builder.RegisterComposite<IWordsPreprocessor>((_, processors) => new CombinedPreprocessor(processors));
        }

        private static IWordsProvider GetWordsFromFileProviderForFile(string pathToFile)
        {
            if (pathToFile == null) throw new ArgumentNullException(nameof(pathToFile));
            var extension = Path.GetExtension(pathToFile)[1..];
            return extension switch
            {
                "txt"  => new WordsFromTxtFileProvider(pathToFile),
                "doc"  => new WordsFromDocFileProvider(pathToFile),
                "docx" => new WordsFromDocFileProvider(pathToFile),
                "pdf"  => new WordsFromPdfFileProvider(pathToFile),
                _      => throw new Exception($"Cannot find file reader for *.{extension} not found")
            };
        }
    }
}