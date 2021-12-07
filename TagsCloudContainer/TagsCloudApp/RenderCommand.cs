using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Rendering;
using TagsCloudContainer.Settings;
using TagsCloudContainer.Settings.Interfaces;
using TagsCloudContainer.WordsLoading;
using TagsCloudVisualization;

namespace TagsCloudApp
{
    public class RenderCommand
    {
        private readonly RenderOptions renderOptions;

        public RenderCommand(RenderOptions renderOptions)
        {
            this.renderOptions = renderOptions;
        }

        public void Render()
        {
            var services = ConfigureServices();
            var provider = services.BuildServiceProvider();
            var tagsCloudDirector = provider.GetRequiredService<ITagsCloudDirector>();
            tagsCloudDirector.Render();
        }

        private IServiceCollection ConfigureServices()
        {
            return new ServiceCollection()
                .AddTransient<Random>()
                .AddTransient<ITagsCloudDirector, TagsCloudDirector>()
                .AddTransient<IWordSpeechPartParser, WordSpeechPartParser>()
                .AddTransient<RandomWordColorMapper>()
                .AddTransient<SpeechPartWordColorMapper>()
                .AddTransient<IObjectParser<SpeechPart>, SpeechPartParser>()
                .AddTransient<IObjectParser<ImageFormat>, ImageFormatParser>()
                .AddTransient<IObjectParser<Color>, ArgbColorParser>()
                .AddTransient<IFontSizeSelector, FrequencyFontSizeSelector>()
                .AddTransient<IWordsColorSettings, WordsColorSettings>()
                .AddTransient<IFileLoaderFactory, FileLoaderFactory>()
                .AddTransient<IWordsPreprocessor, SpeechPartWordsFilter>()
                .AddTransient<IWordsPreprocessor, LowerCaseWordsPreprocessor>()
                .AddTransient<ITagsCloudImageSaver, TagsCloudImageSaver>()
                .AddTransient<ITagsCloudRenderer, TagsCloudRenderer>()
                .AddTransient<ITagsCloudLayouter, FontBasedLayouter>()
                .AddTransient<ICloudLayouter, CircularCloudLayouter>()
                .AddTransient<FrequencyFontSizeSelector>()
                .AddTransient<IObjectParser<IScalersFactory>>(_ => new MappedObjectParser<IScalersFactory>(
                    new Dictionary<string, IScalersFactory>
                    {
                        ["linear"] = new ScalersFactory((start, end) => new LinearScaler(start, end)),
                        ["ln"] = new ScalersFactory((start, end) => new LogScaler(new LinearScaler(start, end)))
                    }))
                .AddTransient<IObjectParser<IWordColorMapper>>(s => new MappedObjectParser<IWordColorMapper>(
                    new Dictionary<string, IWordColorMapper>
                    {
                        ["random"] = s.GetRequiredService<RandomWordColorMapper>(),
                        ["speechPart"] = s.GetRequiredService<SpeechPartWordColorMapper>()
                    }))
                .AddTransient<ISpeechPartWordColorMapperSettings>(_ => new SpeechPartWordColorMapperSettings(
                    new Dictionary<SpeechPart, Color>
                    {
                        [SpeechPart.S] = Color.Crimson,
                        [SpeechPart.V] = Color.SlateBlue
                    }, Color.Aqua))
                .AddTransient(s =>
                    s.GetRequiredService<IObjectParser<IWordColorMapper>>().Parse(renderOptions.ColorMapperType))
                .AddTransient(s =>
                    s.GetRequiredService<IObjectParser<IScalersFactory>>().Parse(renderOptions.WordsScale))
                .AddTransient<IFileLoadSettings>(_ => new FileLoadSettings(renderOptions.InputPath))
                .AddTransient<ISpeechPartFilterSettings>(s =>
                    new SpeechPartFilterSettings(renderOptions.IgnoredSpeechParts
                        .Select(speechPart =>
                            s.GetRequiredService<IObjectParser<SpeechPart>>().Parse(speechPart)).ToHashSet()))
                .AddTransient<IFontSizeSettings>(_ =>
                    new FontSizeSettings(renderOptions.MaxFontSize, renderOptions.MinFontSize))
                .AddTransient<IFontFamilySettings>(_ => new FontFamilySettings(renderOptions.FontFamily))
                .AddTransient<IRenderingSettings>(s => new RenderingSettings
                {
                    Background = new SolidBrush(s.GetRequiredService<IObjectParser<Color>>()
                        .Parse(renderOptions.BackgroundColor)),
                    DesiredImageSize = renderOptions.ImageSize,
                    Scale = renderOptions.ImageScale
                })
                .AddTransient<ISaveSettings>(s =>
                    new SaveSettings(renderOptions.OutputPath,
                        s.GetRequiredService<IObjectParser<ImageFormat>>().Parse(renderOptions.ImageFormat)));
        }
    }
}