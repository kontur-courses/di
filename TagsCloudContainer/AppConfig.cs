using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public class AppConfig
{
    public readonly Parser InputParser;
    public readonly Size FieldSize;
    
    public readonly ILayouter Layouter;
    public readonly HashSet<Color> Colors;
    public readonly HashSet<Font> Fonts;
    
    public AppConfig()
    {
        Console.WriteLine("Введите путь: ");
        var path = Console.ReadLine();
        Console.WriteLine("Введите части речи, которые будут проигнорированы. Доступны: предл, мест, сущ, гл, прил, нар");
        var spPartsToIgnore = Console.ReadLine().Split().ToHashSet();
        Console.WriteLine("Введите слова, которые будут проигнорированы: ");
        var wordsToIgnore = Console.ReadLine().Split().ToHashSet();
        Console.WriteLine("Введите размер ширину и длину поля: 1920 1080");
        var fieldData = Console.ReadLine().Split().Select(int.Parse).ToArray();
        FieldSize = new Size(fieldData[0], fieldData[1]);
        Console.WriteLine("Введите способ раскладки. Доступны: круг");
        var layoutName = Console.ReadLine();
        Console.WriteLine("Введите цвета, которые будут использованы, по умолчанию будут использоваться случайные. Доступны: красный, синий, черный, желтый, зеленый");
        var colors = Console.ReadLine().Split();
        Console.WriteLine("Введите шрифт. Доступны: TNR, Georgia, Arial");
        var fonts = Console.ReadLine();
        InputParser = new Parser(path, wordsToIgnore, spPartsToIgnore);
    }
}