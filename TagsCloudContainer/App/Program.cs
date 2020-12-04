using System;
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
using TagsCloudContainer.Infrastructure.TextParserToFrequencyDictionary;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var inputFile = Path.GetFullPath(Path.Combine(
                    Directory.GetCurrentDirectory(), "..", "..", "..", "text.txt"));
                //var outputFile = Path.GetFullPath(Path.Combine(
                //    Directory.GetCurrentDirectory(), "..", "..", "..", "cloud.png"));
                var imageSettings = ImageSettings.GetDefaultSettings();
                var imageHolder = new PictureBoxImageHolder();
                var services = new ServiceCollection()
                    .AddSingleton<IDataReader>(new TxtFileReader(inputFile))
                    .AddSingleton<ITextParser, SimpleTextParser>()
                    .AddSingleton<IWordNormalizer, ToLowerWordNormalizer>()
                    .AddSingleton<IWordFilter, SimpleWordFilter>()
                    .AddSingleton<ITextParserToFrequencyDictionary,
                        TextParserToFrequencyDictionary.TextParserToFrequencyDictionary>()
                    .AddSingleton<IFontGetter>(new FontGetter(imageSettings.FontName))
                    .AddSingleton<ICloudGenerator, CloudGenerator.CloudGenerator>()
                    .AddSingleton<ICloudPainter>(new CloudPainter(imageSettings))
                    .AddSingleton<ICloudLayouterFactory, CloudLayouterFactory>()
                    .AddSingleton<ICloudVisualizer, CloudVisualizer.CloudVisualizer>()
                    .AddSingleton(imageHolder)
                    .AddSingleton<IImageHolder>(imageHolder)
                    .AddSingleton<IUiAction, CircularCloudAction>()
                    .AddSingleton<IUiAction, ImageSettingsAction>()
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