using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudVisualization;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Parsing;
using TagsCloudVisualization.Reading;
using TagsCloudVisualization.Words;


var options = new VisualizationOptions
{
    MinFontSize = 10,
    MaxFontSize = 35,
    CanvasSize = new Size(1000, 1000),
    BackgroundColor = Color.White,
    SpiralStep = 0.01f,
    FontFamily = new FontFamily("Arial"),
    TakeMostPopularWords = 100,
    Palette = new Palette(Brushes.Black)
};
options.Palette.AvailableBrushes.Add(Brushes.Blue);
options.Palette.AvailableBrushes.Add(Brushes.Red);
options.Palette.AvailableBrushes.Add(Brushes.Brown);
options.Palette.AvailableBrushes.Add(Brushes.Chocolate);
options.Palette.AvailableBrushes.Add(Brushes.Green);


var container = new ServiceCollection()
    .AddTransient<ITextReader>(r => new PlainTextFromFileReader(@"G:\Programs C#\GitHub\Kontur\di\TagsCloudVisualizationConsole\PlainText.txt"))
    .AddSingleton<ITextParser, SingleColumnTextParser>()
    .AddSingleton<IWordsLoader, CustomWordsLoader>()
    .AddTransient<IWordsFilter, CustomWordsFilter>()
    .AddSingleton<IWordsSizeCalculator, CustomWordSizeCalculator>()
    .AddSingleton<ICloudLayouter, CircularCloudLayouter>()
    .AddSingleton<ISpiralFormula, ArithmeticSpiral>()
    .AddTransient<TagCloudVisualizations>()
    .BuildServiceProvider();

var visualizations = ActivatorUtilities.CreateInstance<TagCloudVisualizations>(container);

visualizations.DrawCloud(options);