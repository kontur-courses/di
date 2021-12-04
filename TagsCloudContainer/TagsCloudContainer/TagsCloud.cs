using System;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Rendering;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordsLoading;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class TagsCloud
    {
        public ITagsCloudSettings Settings { get; set; }

        private readonly Lazy<TagsCloudDirector> lazyDirector;

        public TagsCloud()
        {
            var services = new ServiceCollection();
            services
                .AddTransient<IWordSpeechPartParser, WordSpeechPartParser>()
                .AddTransient<TagsCloudDirector>()
                .AddTransient<SpeechPartWordsFilter>()
                .AddTransient<ICloudLayouter, CircularCloudLayouter>()
                .AddTransient<IFontSizeSelector, FrequencyFontSizeSelector>()
                .AddTransient<ITagsCloudRenderer, TagsCloudRenderer>()
                .AddTransient<IFileLoaderFactory, FileLoaderFactory>()
                .AddTransient<ITagsCloudImageSaver, TagsCloudImageSaver>()
                // Default Settings
                .AddTransient<DefaultWordsFiltersSettings>()
                .AddTransient<DefaultWordsColorSettings>()
                .AddTransient(s => new Lazy<FontBasedLayouter>(() =>
                    new FontBasedLayouter(s.GetRequiredService<IFontFamilySettings>(),
                        s.GetRequiredService<IFontSizeSelector>(), s.GetRequiredService<ICloudLayouter>())))
                .AddTransient<DefaultTagsCloudLayouterSettings>()
                .AddTransient<DefaultFontSettings>()
                .AddTransient<DefaultFileLoadSettings>()
                .AddTransient<DefaultRenderingSettings>()
                .AddTransient<DefaultSaveSettings>()
                .AddTransient<DefaultWordsScaleSettings>()
                .AddTransient<DefaultTagsCloudSettings>()
                // Settings factories
                .AddTransient(_ => Settings.FileLoading)
                .AddTransient(_ => Settings.WordsFiltering)
                .AddTransient(_ => Settings.Layouting)
                .AddTransient(_ => Settings.WordsColor)
                .AddTransient<IFontFamilySettings>(_ => Settings.Font)
                .AddTransient<IFontSizeSettings>(_ => Settings.Font)
                .AddTransient(_ => Settings.Rendering)
                .AddTransient(_ => Settings.Saving)
                .AddTransient(_ => Settings.WordsScaling);

            var provider = services.BuildServiceProvider();
            Settings = provider.GetRequiredService<DefaultTagsCloudSettings>();
            lazyDirector = new Lazy<TagsCloudDirector>(() => provider.GetRequiredService<TagsCloudDirector>());
        }

        public void Render()
        {
            lazyDirector.Value.Render();
        }
    }
}