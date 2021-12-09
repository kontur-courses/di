using System;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudApp.Parsers;
using TagsCloudApp.Settings;
using TagsCloudApp.WordsLoading;
using TagsCloudContainer;
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
        private readonly RenderOptions renderOptions;

        public RenderServicesConfigurator(RenderOptions renderOptions)
        {
            this.renderOptions = renderOptions;
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
                .AddSingleton<IFileTextLoaderResolver, FileTextLoaderResolver>()
                .AddSingleton<IFileTextLoader, TxtFileTextLoader>()
                .AddSingleton<IWordsScaleSettings, WordsScaleSettings>()
                .AddSingleton<IMathFunction, LinearFunction>()
                .AddSingleton<IMathFunction, LnFunction>()
                .AddSingleton<ISaveSettings, SaveSettings>()
                .AddSingleton<IFontFamilySettings, FontFamilySettings>()
                .AddSingleton<IRenderingSettings, RenderingSettings>()
                .AddSingleton<IFontSizeSettings, FontSizeSettings>()
                .AddSingleton<ISpeechPartFilterSettings, SpeechPartFilterSettings>()
                .AddSingleton<IDefaultColorSettings, DefaultColorSettings>()
                .AddSingleton<ISpeechPartColorMapSettings, SpeechPartColorMapSettings>()
                .AddSingleton<IKeyValueParser, KeyValueParser>()
                .AddSingleton<IWordsParser, WordsParser>()
                .AddSingleton<IArgbColorParser, ArgbColorParser>()
                .AddSingleton<IImageFormatParser, ImageFormatParser>()
                .AddSingleton<IWordSpeechPartParser, WordSpeechPartParser>()
                .AddSingleton<IEnumParser, EnumParser>()
                .AddSingleton<IMathFunctionResolver, MathFunctionResolver>()
                .AddSingleton(
                    s =>
                        new Lazy<IMathFunctionResolver>(() => new MathFunctionResolver(s.GetServices<IMathFunction>())))
                .AddSingleton<IWordsProvider, FileWordsProvider>()
                .AddSingleton<IBitmapSaver, BitmapSaver>()
                .AddSingleton<IRenderCommand, RenderCommand>()
                .AddSingleton<ITagsCloudLayouter, FontBasedLayouter>()
                .AddSingleton<IFontSizeSelector, FrequencyFontSizeSelector>()
                .AddSingleton<ICloudLayouter, CircularCloudLayouter>()
                .AddSingleton<ITagsCloudRenderer, TagsCloudRenderer>()
                .AddSingleton<Random>()
                .AddSingleton<ITagsCloudDirector, TagsCloudDirector>()
                .AddSingleton<IRenderOptions>(_ => renderOptions);
        }
    }
}