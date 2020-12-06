using System;
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
                //    Directory.GetCurrentDirectory(), "..", "..", "..", "cloud.png"));
                var imageHolder = new PictureBoxImageHolder(ImageSizeSettings.Instance, ImageFormatSettings.Instance);
                var services = new ServiceCollection()
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
                    .AddSingleton<IDataReaderFactory, DataReaderFactory>()
                    .AddSingleton<ITextParser, SimpleTextParser>()
                    .AddSingleton<IWordNormalizer, ToLowerWordNormalizer>()
                    .AddSingleton<IWordFilter, SimpleWordFilter>()
                    .AddSingleton<ITextParserToFrequencyDictionary,
                        TextParserToFrequencyDictionary.TextParserToFrequencyDictionary>()
                    .AddSingleton<IFontGetter, FontGetter>()
                    .AddSingleton<ICloudGenerator, CloudGenerator.CloudGenerator>()
                    .AddSingleton<ICloudPainter, CloudPainter>()
                    .AddSingleton<ICloudLayouterFactory, CloudLayouterFactory>()
                    .AddSingleton<ICloudVisualizer, CloudVisualizer.CloudVisualizer>()
                    .AddSingleton(imageHolder)
                    .AddSingleton<IImageHolder>(imageHolder)
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