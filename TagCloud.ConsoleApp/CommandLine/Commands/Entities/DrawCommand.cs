using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;
using TagCloud.Domain.Visualizer.Interfaces;
using TagCloud.Utils.Files.Interfaces;
using TagCloud.Utils.Images.Interfaces;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class DrawCommand : ICommand
{
    private readonly IVisualizer visualizer;
    private readonly IImageWorker imageWorker;
    private readonly PathSettings pathSettings;
    private readonly IWordsService wordsService;

    public DrawCommand(
        IVisualizer visualizer, 
        IImageWorker imageWorker, 
        PathSettings pathSettings,
        IWordsService wordsService)
    {
        this.visualizer = visualizer;
        this.imageWorker = imageWorker;
        this.pathSettings = pathSettings;
        this.wordsService = wordsService;
    }
    
    public string Trigger => "draw";
    
    public bool Execute(string[] parameters)
    {
        using var image = visualizer.Visualize(wordsService.GetWords(pathSettings.FileFrom));
        imageWorker.SaveImage(image, pathSettings.PathToFile, pathSettings.FileName);
        
        Console.WriteLine($"Изображение было сохранено по пути {Path.GetFullPath(Path.Combine(pathSettings.PathToFile, pathSettings.FileName))}");

        return true;
    }

    public string GetHelp()
    {
        return "С помощью этой команды будет нарисовано облако тегов\n" +
               "Не имеет параметров";
    }
}