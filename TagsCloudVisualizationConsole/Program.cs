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
var options = VisualizationOptionsConverter.ConvertOptions(appOptions!);

var container = new ServiceCollection()
    .AddTransient<ITextReader>(r => new PlainTextFromFileReader(appOptions!.PathToTextFile))
    .AddSingleton<ITextParser, SingleColumnTextParser>()
    .AddSingleton<IWordsLoader, CustomWordsLoader>()
    .AddTransient<IWordsFilter, CustomWordsFilter>()
    .AddTransient<IMorphAnalyzer>(r => new MyStemMorphAnalyzer(appOptions!.DirectoryToMyStemProgram))
    .AddSingleton<IWordsSizeCalculator, CustomWordSizeCalculator>()
    .AddSingleton<ICloudLayouter, CircularCloudLayouter>()
    .AddSingleton<ISpiralFormula, ArithmeticSpiral>()
    .AddTransient<TagCloudVisualizations>()
    .BuildServiceProvider();

var visualizations = ActivatorUtilities.CreateInstance<TagCloudVisualizations>(container);

var bitmap = visualizations.DrawCloud(options);

bitmap.Save(Path.Combine(appOptions!.DirectoryToSaveFile, string.Concat(appOptions.SaveFileName, ".", appOptions.FileExtension.ToLower())), AppOptionsValidator.GetImageFormat(appOptions.FileExtension));