using TagCloudContainer.Additions.Interfaces;

namespace TagCloudContainer.Additions;

public class TagCloudContainerConfig : ITagCloudContainerConfig
{
    public bool Random { get; set; } = true;
    public Size StandartSize { get; set; } = new Size(10, 10);
    public Point Center { get; set; } = new Point(1, 1);
    public SortedList<float, Point> NearestToTheCenterPoints { get; set; } = new SortedList<float, Point>();
    public List<Rectangle> PutRectangles { get; set; } = new List<Rectangle>();
}