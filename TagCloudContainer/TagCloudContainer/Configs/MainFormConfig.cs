namespace TagCloudContainer;

public static class MainFormConfig
{
    public static string FontFamily { get; set; } = "Arial";
    public static Color Color { get; set; } = Colors.Get("Black");
    public static Color BackgroundColor { get; set; } = Colors.Get("White");
    public static Size FormSize { get; set; } = Sizes.Get("1920x1080");
    public static string FileName { get; set; } = "words.txt";
    public static string ExcludeWordsFileName { get; set; } = "boring_words.txt";
    public static bool NeedValidate { get; set; } = true;
    public static Point Center { get; set; } = new Point(1, 1);
    public static Size StandartSize { get; set; } = new Size(10, 10);
    public static bool Random { get; set; } = true;
    public static SortedList<float, Point> NearestToTheCenterPoints { get; set; } = new SortedList<float, Point>();
    public static List<Rectangle> PutRectangles { get; set; } = new List<Rectangle>();
}