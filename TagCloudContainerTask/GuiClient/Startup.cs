using System;
using App.Implementation;
using App.Implementation.FileInteractions.Readers;
using App.Implementation.FileInteractions.Writers;
using App.Implementation.LayoutingAlgorithms;
using App.Implementation.SettingsHolders;
using App.Implementation.Visualization;
using App.Implementation.Words.Filters;
using App.Implementation.Words.FrequencyAnalyzers;
using App.Implementation.Words.Preprocessors;
using App.Implementation.Words.Tags;
using App.Infrastructure;
using App.Infrastructure.FileInteractions.Readers;
using App.Infrastructure.FileInteractions.Writers;
using App.Infrastructure.LayoutingAlgorithms;
using App.Infrastructure.SettingsHolders;
using App.Infrastructure.Visualization;
using App.Infrastructure.Words.Filters;
using App.Infrastructure.Words.FrequencyAnalyzers;
using App.Infrastructure.Words.Preprocessors;
using App.Infrastructure.Words.Tags;
using GuiClient.UiActions;
using Microsoft.Extensions.DependencyInjection;

namespace GuiClient
{
    public static class Startup
    {
        public static ServiceProvider GetAppServiceProvider()
        {
            var imageSettings = new ImageSizeSettings();
            var fontSettings = new FontSettings();
            var inputFileSettings = new InputFileSettings();
            var outputResultSettings = new OutputResultSettings();
            var paletteSettings = new PaletteSettings();

            return new ServiceCollection()
                .AddSingleton(imageSettings)
                .AddSingleton<IImageSizeSettingsHolder>(imageSettings)
                .AddSingleton(fontSettings)
                .AddSingleton<IFontSettingsHolder>(fontSettings)
                .AddSingleton(inputFileSettings)
                .AddSingleton<IInputFileSettingsHolder>(inputFileSettings)
                .AddSingleton(outputResultSettings)
                .AddSingleton<IOutputResultSettingsHolder>(outputResultSettings)
                .AddSingleton(paletteSettings)
                .AddSingleton<IPaletteSettingsHolder>(paletteSettings)
                .AddSingleton<ITagger, Tagger>()
                .AddSingleton<IReaderFactory, ReaderFactory>()
                .AddSingleton<ILineWriter, LineWriter>()
                .AddSingleton<IPreprocessor, ToLowerCasePreprocessor>()
                .AddSingleton<IPreprocessor, ToInitFormPreprocessor>()
                .AddSingleton<IFilter, WordsLengthFilter>()
                .AddSingleton<IFrequencyAnalyzer, WordsFrequencyAnalyzer>()
                .AddSingleton<ICloudGenerator, CloudGenerator>()
                .AddSingleton<IDrawer, Drawer>()
                .AddSingleton<ILayouterFactory, LayouterFactory>()
                .AddSingleton<IVisualizer, Visualizer>()
                .AddSingleton(provider => new Lazy<MainForm>(() => provider.GetRequiredService<MainForm>()))
                .AddSingleton<PictureBoxImageHolder, PictureBoxImageHolder>()
                .AddSingleton<IImageHolder>(x => x.GetRequiredService<PictureBoxImageHolder>())
                .AddSingleton(provider => new Lazy<IImageHolder>(() => provider.GetRequiredService<IImageHolder>()))
                .AddSingleton<IUiAction, ImageSettingsAction>()
                .AddSingleton<IUiAction, PaletteSettingsAction>()
                .AddSingleton<IUiAction, FontSettingsAction>()
                .AddSingleton<IUiAction, SaveImageAction>()
                .AddSingleton<IUiAction, OpenFileAction>()
                .AddSingleton<MainForm, MainForm>()
                .BuildServiceProvider();
        }
    }
}