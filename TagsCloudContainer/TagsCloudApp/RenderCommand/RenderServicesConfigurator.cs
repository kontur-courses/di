using System;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudApp.Parsers;
using TagsCloudApp.Settings;
using TagsCloudApp.WordsLoading;
using TagsCloudContainer;
using TagsCloudContainer.ColorMappers;
using TagsCloudContainer.DependencyInjection;
using TagsCloudContainer.Layout;
using TagsCloudContainer.MathFunctions;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Rendering;
using TagsCloudContainer.Settings;
using TagsCloudVisualization;

namespace TagsCloudApp.RenderCommand
{
    public class RenderServicesConfigurator
    {
        private readonly RenderArgs renderArgs;

        public RenderServicesConfigurator(RenderArgs renderArgs)
        {
            this.renderArgs = renderArgs;
        }

        public IServiceCollection ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<IWordColorMapperSettings, WordColorMapperSettings>()
                .AddSingleton<IWordColorMapper, StaticWordColorMapper>()
                .AddSingleton<IWordColorMapper, RandomWordColorMapper>()
                .AddSingleton<IWordColorMapper, SpeechPartWordColorMapper>()
                .AddSingleton<IWordsPreprocessor, LowerCaseWordsPreprocessor>()
                .AddSingleton<IWordsPreprocessor, SpeechPartWordsFilter>()
                .AddSingleton<IFileTextLoader, TxtFileTextLoader>()
                .AddSingleton<IWordsScaleSettings, WordsScaleSettings>()
                .AddSingleton<IMathFunction, LinearFunction>()
                .AddSingleton<IMathFunction, LnFunction>()
                .AddSingleton<ISaveSettings, SaveSettings>()
                .AddSingleton<IRenderSettingsConverter, RenderSettingsConverter>()
                .AddSingleton(p => p.GetRequiredService<IRenderSettingsConverter>().GetSettings())
                .AddSingleton<IFontFamilySettings, FontFamilySettings>()
                .AddSingleton<IRenderingSettings, RenderingSettings>()
                .AddSingleton<IFontSizeSettings, FontSizeSettings>()
                .AddSingleton<ISpeechPartFilterSettings, SpeechPartFilterSettings>()
                .AddSingleton<IDefaultColorSettings, DefaultColorSettings>()
                .AddSingleton<ISpeechPartColorMapSettings, SpeechPartColorMapSettings>()
                .AddSingleton(
                    s => s.GetRequiredService<IServiceResolver<TagsCloudLayouterType, ITagsCloudLayouter>>()
                        .GetService(TagsCloudLayouterType.FontBased))
                .AddSingleton<IKeyValueParser, KeyValueParser>()
                .AddSingleton<IWordsParser, WordsParser>()
                .AddSingleton<IArgbColorParser, ArgbColorParser>()
                .AddSingleton<IImageFormatParser, ImageFormatParser>()
                .AddSingleton<IWordSpeechPartParser, WordSpeechPartParser>()
                .AddSingleton<IEnumParser, EnumParser>()
                .AddSingleton(typeof(IServiceResolver<,>), typeof(ServiceResolver<,>))
                .AddSingleton(typeof(Lazy<>), typeof(LazyResolver<>))
                .AddSingleton<IFileTextLoaderResolver, FileTextLoaderResolver>()
                .AddSingleton<IWordsProvider, FileWordsProvider>()
                .AddSingleton<IBitmapSaver, BitmapSaver>()
                .AddSingleton<IRenderCommand, RenderCommand>()
                .AddSingleton<ITagsCloudLayouter, FontBasedLayouter>()
                .AddSingleton<IFontSizeSelector, FrequencyFontSizeSelector>()
                .AddSingleton<ICloudLayouter, CircularCloudLayouter>()
                .AddSingleton<ITagsCloudRenderer, TagsCloudRenderer>()
                .AddSingleton<Random>()
                .AddSingleton<ITagsCloudDirector, TagsCloudDirector>()
                .AddSingleton<IRenderArgs>(_ => renderArgs);
        }
    }
}