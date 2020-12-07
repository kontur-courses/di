﻿using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.App.Actions;
using TagsCloudContainer.App.CloudGenerator;
using TagsCloudContainer.App.CloudVisualizer;
using TagsCloudContainer.App.DataReader;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.App.TextParserToFrequencyDictionary;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.CloudGenerator;
using TagsCloudContainer.Infrastructure.CloudVisualizer;
using TagsCloudContainer.Infrastructure.DataReader;
using TagsCloudContainer.Infrastructure.Settings;
using TagsCloudContainer.Infrastructure.TextParserToFrequencyDictionary;
using TagsCloudContainer.Infrastructure.UiActions;
using YandexMystem.Wrapper;

namespace TagsCloudContainer.App
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                //var inputFile = Path.GetFullPath(Path.Combine(
                //    Directory.GetCurrentDirectory(), "..", "..", "..", "text.txt"));
                //var outputFile = Path.GetFullPath(Path.Combine(
                //    Directory.GetCurrentDirectory(), "..", "..", "..", "cloud.png")););
                var services = new ServiceCollection()
                    .AddSingleton<Mysteam>(new Mysteam(Path.GetFullPath(Path.Combine(
                        Directory.GetCurrentDirectory(), "..", "..", "..", "mystem.exe"))))
                    .AddSingleton(ImageSizeSettings.Instance)
                    .AddSingleton<IImageSizeSettingsHolder> (ImageSizeSettings.Instance)
                    .AddSingleton(FontSettings.Instance)
                    .AddSingleton<IFontSettingsHolder>(FontSettings.Instance)
                    .AddSingleton(ImageFormatSettings.Instance)
                    .AddSingleton<IImageFormatSettingsHolder>(ImageFormatSettings.Instance)
                    .AddSingleton(InputSettings.Instance)
                    .AddSingleton<IInputSettingsHolder>(InputSettings.Instance)
                    .AddSingleton(LayouterAlgorithmSettings.Instance)
                    .AddSingleton<ILayouterAlgorithmSettingsHolder>(LayouterAlgorithmSettings.Instance)
                    .AddSingleton(OutputSettings.Instance)
                    .AddSingleton<IOutputSettingsHolder>(OutputSettings.Instance)
                    .AddSingleton(Palette.Instance)
                    .AddSingleton<IPaletteSettingsHolder>(Palette.Instance)
                    .AddSingleton(FilteringWordsSettings.Instance)
                    .AddSingleton<IFilteringWordsSettingsHolder>(FilteringWordsSettings.Instance)
                    .AddSingleton<IDataReaderFactory, DataReaderFactory>()
                    .AddSingleton<ITextParser, SimpleTextParser>()
                    .AddSingleton<IWordNormalizer, ToLowerWordNormalizer>()
                    .AddSingleton<IWordFilter, PartOfSpeechFilter>()
                    .AddSingleton<ITextParserToFrequencyDictionary,
                        TextParserToFrequencyDictionary.TextParserToFrequencyDictionary>()
                    .AddSingleton<IFontGetter, FontGetter>()
                    .AddSingleton<ICloudGenerator, CloudGenerator.CloudGenerator>()
                    .AddSingleton<ICloudPainter, CloudPainter>()
                    .AddSingleton<ICloudLayouterFactory, CloudLayouterFactory>()
                    .AddSingleton<ICloudVisualizer, CloudVisualizer.CloudVisualizer>()
                    .AddSingleton(provider => new Lazy<MainForm>(() => provider.GetRequiredService<MainForm>()))
                    .AddSingleton<PictureBoxImageHolder, PictureBoxImageHolder>()
                    .AddSingleton<IImageHolder>(x => x.GetRequiredService<PictureBoxImageHolder>())
                    .AddSingleton(provider => new Lazy<IImageHolder>(() => provider.GetRequiredService<IImageHolder>()))
                    .AddSingleton<IUiAction, CircularCloudAction>()
                    .AddSingleton<IUiAction, ImageSettingsAction>()
                    .AddSingleton<IUiAction, PaletteSettingsAction>()
                    .AddSingleton<IUiAction, FontSettingsAction>()
                    .AddSingleton<IUiAction, SaveImageAction>()
                    .AddSingleton<IUiAction, OpenFileAction>()
                    .AddSingleton<MainForm, MainForm>();
                var serviceProvider = services.BuildServiceProvider();
                var mainForm = serviceProvider.GetService<MainForm>();

                Application.Run(mainForm);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}