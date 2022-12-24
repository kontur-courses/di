using TagCloudContainer.Core.Interfaces;
using TagCloudContainer.Core.Models;

namespace TagCloudContainer.Configs;

public class TagCloudContainerConfig : ITagCloudContainerConfig, ITagCloudFormConfig
{
    public bool Random { get; set; } = true;
    
    public Size StandartSize { get; set; } = new Size(10, 10);
    public Size ImageSize { get; set; } = new Size(int.Parse(Screens.Sizes.First().Split("x")[0]), 
        int.Parse(Screens.Sizes.First().Split("x")[1]));
    
    public Point Center { get; set; } = new Point(1, 1);
    
    public SortedList<float, Point> NearestToTheCenterPoints { get; set; } = new SortedList<float, Point>();
    
    public List<Rectangle> PutRectangles { get; set; } = new List<Rectangle>();
    
    public string FilePath { get; set; } = Path.Combine(GetMainDirectoryPath(), "words.txt");
    
    public string ExcludeWordsFilePath { get; set; } = Path.Combine(GetMainDirectoryPath(), "boring_words.txt");
    
    public bool NeedValidate { get; set; } = true;

    public string FontFamily { get; set; } = "Arial";
    public Color Color { get; set; } = Colors.GetAll().First().Value;
    public Color BackgroundColor { get; set; } = Colors.GetAll().First().Value;
    
    public void SetFilePath(string fileName)
    {
        FilePath = Path.Combine(GetMainDirectoryPath(), fileName);
    }
    public void SetExcludeWordsFilePath(string fileName)
    {
        ExcludeWordsFilePath = Path.Combine(GetMainDirectoryPath(), fileName);
    }
    public static string GetMainDirectoryPath()
    {
        return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
    }
}