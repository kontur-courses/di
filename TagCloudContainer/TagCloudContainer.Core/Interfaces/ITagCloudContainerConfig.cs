namespace TagCloudContainer.Core.Interfaces;

public interface ITagCloudContainerConfig
{
    public bool Random { get; set; }
    public Size StandartSize { get; set; } 
    public Size ImageSize { get; set; }
    public string MainDirectoryPath { get; set; }
    public string ImageName { get; set; }
    public Point Center { get; set; } 
    public SortedList<float, Point> NearestToTheCenterPoints { get; set; }
    public List<Rectangle> PutRectangles { get; set; }
    public string FilePath { get; set; }
    public string ExcludeWordsFilePath { get; set; }
    public bool NeedValidate { get; set; }
    public void SetFilePath(string fileName);
    public void SetExcludeWordsFilePath(string fileName);
}