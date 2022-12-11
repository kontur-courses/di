using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudVisualization;
using TagsCloudVisualization.CLI;
using TagsCloudVisualization.CLI.Extensions;

var result = Parser.Default.ParseArguments<Options>(args);

var settings = result.Value.GetVisualizationSettings();

var directoryPath = Path.GetFullPath(settings.OutputDirectory);
if (!Directory.Exists(directoryPath))
{
    Directory.CreateDirectory(directoryPath);
}

var services = new ServiceCollection();
services.AddTagCloudVisualization(settings);
var filepath = Path.Combine(directoryPath, CreateFileName());

var visualizer = services.BuildServiceProvider().GetRequiredService<Visualizer>();
visualizer.Visualize(filepath, settings.TagCount);

string CreateFileName()
{
    return $"Cloud_{DateTime.Now:dd-MM-yy_hh-mm-ss}";
}