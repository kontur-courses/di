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
    private readonly FileSettings fileSettings;
    private readonly IWordsService wordsService;

    public DrawCommand(
        IVisualizer visualizer, 
        IImageWorker imageWorker, 
        FileSettings fileSettings,
        IWordsService wordsService)
    {
        this.visualizer = visualizer;
        this.imageWorker = imageWorker;
        this.fileSettings = fileSettings;
        this.wordsService = wordsService;
    }
    
    public string Trigger => "draw";
    
    public bool Execute(string[] parameters)
    {
        using var image = visualizer.Visualize(wordsService.GetWords(fileSettings.FileFromWithPath));
        imageWorker.SaveImage(image, fileSettings.OutPathToFile, fileSettings.ImageFormat, fileSettings.OutFileName);
        
        Console.WriteLine($"Изображение было сохранено по пути {Path.GetFullPath(Path.Combine(fileSettings.OutPathToFile, fileSettings.OutFileName))}");

        return true;
    }

    public string GetHelp()
    {
        return GetShortHelp() + Environment.NewLine + 
               "Не имеет параметров";
    }
    
    public string GetShortHelp()
    {
        return Trigger + " позволяет нарисовать облако тегов";
    }
}