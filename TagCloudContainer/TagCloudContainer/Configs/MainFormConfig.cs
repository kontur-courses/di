namespace TagCloudContainer;

public class MainFormConfig : IMainFormConfig
{
    public string FontFamily { get; set; } = "Arial";
    public Color Color { get; set; } = Colors.Get("Black");
    public Color BackgroundColor { get; set; } = Colors.Get("White");
    public Size FormSize { get; set; } = Screen.PrimaryScreen.Bounds.Size;
    public string FileName { get; set; } = "words.txt";
    public string ExcludeWordsFileName { get; set; } = "boring_words.txt";
    public bool NeedValidate { get; set; } = true;
    public Point Center { get; set; } = new Point(1, 1);
    public Size StandartSize { get; set; } = new Size(10, 10);
    public bool Random { get; set; } = true;
    public SortedList<float, Point> NearestToTheCenterPoints { get; set; } = new SortedList<float, Point>();
    public List<Rectangle> PutRectangles { get; set; } = new List<Rectangle>();
}