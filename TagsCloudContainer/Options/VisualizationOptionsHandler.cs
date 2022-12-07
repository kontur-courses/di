using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.Options;

public static class VisualizationOptionsHandler
{
    public static VisualizationOptions RequestVisualizationOptions()
    {
        return new VisualizationOptions(RequestPathToTextFile(), RequestSaveImageFormat(), RequestImageSize(),
            RequestLengthOfBoringWords(), RequestMinFontSize());
    }

    private static ImageFormat RequestSaveImageFormat()
    {
        Console.WriteLine("Введите формат изображения(.bmp/.jpeg/.png):");
        var input = Console.ReadLine();
        return input switch
        {
            ".bmp" => ImageFormat.Bmp,
            ".jpeg" => ImageFormat.Jpeg,
            ".png" => ImageFormat.Png,
            _ => throw new BadImageFormatException("Недопустимый формат изображения")
        };
    }

    private static string RequestPathToTextFile()
    {
        Console.WriteLine("Введите путь к текстовому файлу:");
        var path = Console.ReadLine();
        return File.Exists(path)
            ? path
            : throw new FileNotFoundException("Указанного файла не существует");
    }

    private static int RequestMinFontSize()
    {
        Console.WriteLine("Введите начальный размер шрифта:");
        var input = Console.ReadLine();
        return string.IsNullOrEmpty(input)
            ? throw new ArgumentException("Некорректный размер шрифта")
            : int.Parse(input);
    }

    private static Size RequestImageSize()
    {
        Console.WriteLine("Введите желаемый размер изображения в формате \"width, height\"");
        var sizes = Console.ReadLine()?.Split(", ").Select(int.Parse).ToArray();
        return sizes is { Length: 2 }
            ? new Size(sizes[0], sizes[1])
            : throw new ArgumentException("Неправильный размер изображения");
    }

    private static int RequestLengthOfBoringWords()
    {
        Console.WriteLine("Введите минимальную длину слова(можно пропустить):");
        var input = Console.ReadLine();
        return string.IsNullOrEmpty(input)
            ? int.MinValue
            : int.Parse(input);
    }
}