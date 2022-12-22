namespace TagCloudContainer.Additions.Interfaces;

public interface ITagCloudContainerConfig
{
    public bool Random { get; set; }
    public Size StandartSize { get; set; } 
    public Point Center { get; set; } 
    public SortedList<float, Point> NearestToTheCenterPoints { get; set; }
    public List<Rectangle> PutRectangles { get; set; }
}