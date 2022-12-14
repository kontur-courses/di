using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TagsCloudVisualization;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.MorphAnalyzer;
using TagsCloudVisualization.Parsing;
using TagsCloudVisualization.Reading;
using TagsCloudVisualization.Words;
using TagsCloudVisualizationConsole;


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddCommandLine(args)
    .Build();


var appOptions = configuration.Get<ArgsOptions>();
AppOptionsValidator.ValidatePathsInOptions(appOptions);
var options2 = VisualizationOptionsConverter.ConvertOptions(appOptions!);

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
    .AddTransient<ITextReader>(r => new PlainTextFromFileReader(appOptions!.PathToTextFile))
    .AddSingleton<ITextParser, SingleColumnTextParser>()
    .AddSingleton<IWordsLoader, CustomWordsLoader>()
    .AddTransient<IWordsFilter, CustomWordsFilter>()
    .AddTransient<IMorphAnalyzer>(r=> new MyStemMorphAnalyzer(appOptions!.DirectoryToMyStemProgram))
    .AddSingleton<IWordsSizeCalculator, CustomWordSizeCalculator>()
    .AddSingleton<ICloudLayouter, CircularCloudLayouter>()
    .AddSingleton<ISpiralFormula, ArithmeticSpiral>()
    .AddTransient<TagCloudVisualizations>()
    .BuildServiceProvider();

var visualizations = ActivatorUtilities.CreateInstance<TagCloudVisualizations>(container);

var bitmap = visualizations.DrawCloud(options2);

bitmap.Save(Path.Combine(appOptions!.DirectoryToSaveFile, appOptions.SaveFileName));