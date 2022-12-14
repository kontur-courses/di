using System.Drawing;
using CommandLine;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public class CommandLineOptions
{
    [Option('i', "input", Required = true, HelpText = "Путь к файлу со словами")]
    public string InputFile { get; set; }

    [Option('o', "output", Required = false,
        HelpText = "Путь куда файл будет сохранён и тип файла. Доступны: png, jpg, tiff, bmp, raw")]
    public string OutputFile { get; set; }

    [Option("ignoreWordsPath", Required = false,
        HelpText = "Путь к файлу, в котором указаны слова, которые нужно проигнорировать")]
    public string? WordsToIgnoreFile { get; set; }

    [Option("ignoreSpPartsPath", Required = false,
        HelpText =
            "Путь к файлу, в котором указаны части речи, которые нужно проигнорировать. Доступны: предл, мест, сущ, гл, прил, нар")]
    public string? SpPartsToIgnoreFile { get; set; }

    [Option('c', "colorsPath", Required = false, Default = null,
        HelpText =
            "Путь к файлу, в котором указаны цвета, которые будут использованы. Доступны: красный, синий, черный, желтый, зеленый")]
    public string? Colors { get; set; }

    [Option('h', "height", Required = false, Default = 1080, HelpText = "Высота изображения")]
    public int Height { get; set; }

    [Option('w', "width", Required = false, Default = 1920, HelpText = "Ширина изображения")]
    public int Width { get; set; }

    [Option("fontName", Required = false, Default = "Times New Roman", HelpText = "Шрифт")]
    public string? FontName { get; set; }

    [Option('l', "layout", Required = false, Default = "Circle", HelpText = "Алгоритм раскладки")]
    public string? LayouterName { get; set; }

    [Option("step", Required = false, Default = 1, HelpText = "Шаг раскладки")]
    public double Step { get; set; }

    [Option("density", Required = false, Default = 1, HelpText = "Кучность раскладки")]
    public double Density { get; set; }

    [Option("start", Required = false, Default = 0, HelpText = "Начало отсчёта")]
    public double Start { get; set; }

    public ISizeManager SizeManager { get; set; }

    public HashSet<string> SpPartsToIgnore { get; set; }

    public HashSet<string> WordsToIgnore { get; set; }
    public HashSet<Brush> ColorsParsed { get; set; }
}