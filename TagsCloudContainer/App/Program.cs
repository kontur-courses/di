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
                var imageHolder = new PictureBoxImageHolder();
                var services = new ServiceCollection()
                    .AddSingleton<IDataReaderFactory, DataReaderFactory>()
                    .AddSingleton<ITextParser, SimpleTextParser>()
                    .AddSingleton<IWordNormalizer, ToLowerWordNormalizer>()
                    .AddSingleton<IWordFilter, SimpleWordFilter>()
                    .AddSingleton<ITextParserToFrequencyDictionary,
                        TextParserToFrequencyDictionary.TextParserToFrequencyDictionary>()
                    .AddSingleton<IFontGetter>(new FontGetter(AppSettings.Default))
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