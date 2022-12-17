namespace TagCloudContainer;

public interface IMainFormConfig
{
    public string FontFamily { get; set; }
    public Color Color { get; set; } 
    public Color BackgroundColor { get; set; } 
    public Size FormSize { get; set; } 
    public string FileName { get; set; } 
    public string ExcludeWordsFileName { get; set; } 
    public bool NeedValidate { get; set; } 
    public Point Center { get; set; } 
    public Size StandartSize { get; set; } 
    public bool Random { get; set; } 
    public SortedList<float, Point> NearestToTheCenterPoints { get; set; }
    public List<Rectangle> PutRectangles { get; set; }
}