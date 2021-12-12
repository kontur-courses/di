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

            var serviceCollection = new ServiceCollection();

            serviceCollection
                .AddSingleton(fontSettings)
                .AddSingleton(imageSettings)
                .AddSingleton(paletteSettings)
                .AddSingleton(inputFileSettings)
                .AddSingleton(outputResultSettings);

            serviceCollection
                .AddSingleton<IFontSettingsHolder>(fontSettings)
                .AddSingleton<IImageSizeSettingsHolder>(imageSettings)
                .AddSingleton<IPaletteSettingsHolder>(paletteSettings)
                .AddSingleton<IInputFileSettingsHolder>(inputFileSettings)
                .AddSingleton<IOutputResultSettingsHolder>(outputResultSettings);

            serviceCollection
                .AddSingleton<ITagger, Tagger>()
                .AddSingleton<IFilter, WordsLengthFilter>()
                .AddSingleton<IPreprocessor, ToInitFormPreprocessor>()
                .AddSingleton<IPreprocessor, ToLowerCasePreprocessor>()
                .AddSingleton<IFrequencyAnalyzer, WordsFrequencyAnalyzer>();

            serviceCollection
                .AddSingleton<IUiAction, OpenFileAction>()
                .AddSingleton<IUiAction, SaveImageAction>()
                .AddSingleton<IUiAction, RedrawImageAction>()
                .AddSingleton<IUiAction, FontSettingsAction>()
                .AddSingleton<IUiAction, ImageSettingsAction>()
                .AddSingleton<IUiAction, PaletteSettingsAction>();

            serviceCollection
                .AddSingleton<IDrawer, Drawer>()
                .AddSingleton<IVisualizer, Visualizer>()
                .AddSingleton<ICloudGenerator, CloudGenerator>()
                .AddSingleton<ILayouterFactory, LayouterFactory>();

            serviceCollection
                .AddSingleton<ILineWriter, LineWriter>()
                .AddSingleton<IReaderFactory, ReaderFactory>();

            serviceCollection
                .AddSingleton<PictureBoxImageHolder, PictureBoxImageHolder>()
                .AddSingleton<IImageHolder>(x => x.GetRequiredService<PictureBoxImageHolder>())
                .AddSingleton(provider => new Lazy<IImageHolder>(() => provider.GetRequiredService<IImageHolder>()));

            serviceCollection
                .AddSingleton(provider => new Lazy<MainForm>(() => provider.GetRequiredService<MainForm>()))
                .AddSingleton<MainForm, MainForm>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}