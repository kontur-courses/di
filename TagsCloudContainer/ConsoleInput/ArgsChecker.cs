using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer.WordsInterfaces;
using TagsCloudVisualization;

namespace TagsCloudContainer;
#pragma warning disable CA1416
public class ArgsChecker
{
    public CommandLineOptions Check(CommandLineOptions options)
    {
        try
        {
            options.ColorsParsed = GetColors(options.Colors);
            options.FontName = GetFont(options.FontName);
            options.SizeManager = GetSizeManager(options.LayouterName);
            options.OutputFile = GetOutputPath(options.OutputFile);
            options.SpPartsToIgnore = GetSpPartsToIgnore(options.SpPartsToIgnoreFile);
            options.WordsToIgnore = GetWordsToIgnore(options.WordsToIgnoreFile);
            options.Width = GetWidth(options.Width);
            options.Height = GetHeight(options.Height);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return options;
    }

    private HashSet<Brush> GetColors(string? colorsPath)
    {
        var result = new HashSet<Brush>();

        if (colorsPath is null)
            return new HashSet<Brush> { Brushes.Black };

        if (!File.Exists(colorsPath))
            throw new Exception($"Файл с цветами не найден {colorsPath}");

        foreach (var color in AppDIInitializer.Container.GetService<IWordsReader>().Read(colorsPath))
        {
            if (!AvailableOptions.AvailableBrushes.ContainsKey(color))
                throw new Exception($"Неизвестный цвет! {color}");

            result.Add(AvailableOptions.AvailableBrushes[color]);
        }

        return result;
    }

    private string? GetFont(string? fontName)
    {
        if (fontName is null)
            return "Times New Roman";

        return AvailableOptions.AvailableFonts.Contains(fontName)
            ? fontName
            : throw new Exception($"Неизвестный шрифт {fontName}");
    }

    private ISizeManager GetSizeManager(string? layouterName)
    {
        if (layouterName is null)
            return AppDIInitializer.Container.GetService<ISizeManager>();

        return AvailableOptions.AvailableLayouters.TryGetValue(layouterName, out var value)
            ? value
            : throw new Exception($"Неизвестная раскладка {layouterName}");
    }

    private string GetOutputPath(string? outputPath)
    {
        if (outputPath is null)
            return "Result.png";

        var splitted = outputPath.Split('\\');
        var last = splitted.Last();
        var path = string.Join('\\', splitted.SkipLast(1));

        if (!Directory.Exists(path))
            throw new Exception($"Директория для сохранения файла не найдена! {path}");

        if (!AvailableOptions.AvailableSaveFormats.Contains(last.Split('.')[^1]))
            throw new Exception($"Неизвестный формат! {last.Split('.')[^1]}");

        return outputPath;
    }

    private HashSet<string> GetSpPartsToIgnore(string? spPartsToIgnorePath)
    {
        if (spPartsToIgnorePath is null)
            return new HashSet<string>();

        if (!File.Exists(spPartsToIgnorePath))
            throw new Exception($"Файл с частями речи не найден! {spPartsToIgnorePath}");

        var result = new HashSet<string>();
        foreach (var spPart in AppDIInitializer.Container.GetService<IWordsReader>().Read(spPartsToIgnorePath))
        {
            if (!AvailableOptions.AvailableSpPartsToIgnore.Contains(spPart))
                throw new Exception($"Неизвестная часть речи! {spPart}");

            result.Add(spPart);
        }

        return result;
    }

    private HashSet<string> GetWordsToIgnore(string? wordsToIgnorePath)
    {
        if (wordsToIgnorePath is null)
            return new HashSet<string>();

        if (!File.Exists(wordsToIgnorePath))
            throw new Exception($"Файл со словами не найден! {wordsToIgnorePath}");

        return AppDIInitializer.Container.GetService<IWordsReader>()
            .Read(wordsToIgnorePath)
            .ToHashSet();
    }

    private int GetWidth(int width)
    {
        return width switch
        {
            > 23170 => throw new Exception("Ширина не может превышать 23170"),
            >= 0 => width,
            _ => throw new Exception("Ширина не может быть отрицательной!")
        };
    }

    private int GetHeight(int height)
    {
        return height switch
        {
            > 23170 => throw new Exception("Высота не может превышать 23170"),
            >= 0 => height,
            _ => throw new Exception("Высота не может быть отрицательной!")
        };
    }
}

#pragma warning restore CA1416