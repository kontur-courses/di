using System.Drawing;
using System.Text;
using McMaster.Extensions.CommandLineUtils;

namespace TagsCloud2.InputManager;

public class InputManager : IInputManager
{
    private string path;
    private string pathToSave;
    private int size;
    private Brush colorBrush;
    private string fontFamilyName;
    private string formatToSave;
    private bool isVerticalWords;

    public void GatherInformation()
    {
        Console.OutputEncoding = Encoding.UTF8;
        path = GetInputWordsPath();
        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }

        pathToSave = GetDirectoryToSave();
        if (!Directory.Exists(pathToSave))
        {
            throw new DirectoryNotFoundException();
        }

        size = GetSize();
        if (size < 2000 && size > 4000)
        {
            Console.WriteLine("Размер не из диапазона :(");
            throw new ArgumentException();
        }

        isVerticalWords = GetIsVerticalWords();

        var color = GetColor();
        if (color < 1 && color > 2)
        {
            Console.WriteLine("Такого цвета нет :( Пока!");
            throw new ArgumentException();
        }

        colorBrush = ColorBrush(color);

        var font = GetFontFamilyName();
        if (font < 1 && font > 2)
        {
            Console.WriteLine("Такого шрифта нет :( Пока!");
            throw new ArgumentException();
        }

        fontFamilyName = DefineFontFamilyName(font);

        var format = GetFormatToSave();

        if (format < 1 && format > 2)
        {
            Console.WriteLine("Такого формата нет :( Пока!");
            throw new ArgumentException();
        }

        formatToSave = DefineFormatToSave(format);
    }
    public string Path()
    {
        return path;
    }

    public string PathToSave()
    {
        return pathToSave;
    }

    public int Size()
    {
        return size;
    }

    public Brush BrushColor()
    {
        return colorBrush;
    }

    public string FontFamilyName()
    {
        return fontFamilyName;
    }

    public string FormatToSave()
    {
        return formatToSave;
    }

    public bool IsVerticalWords()
    {
        return isVerticalWords;
    }
    
    private static string DefineFormatToSave(int format)
    {
        string formatToSave = "png";

        switch (format)
        {
            case 1:
                formatToSave = "png";
                break;
            case 2:
                formatToSave = "jpg";
                break;
        }

        return formatToSave;
    }

    private static int GetFormatToSave()
    {
        return Prompt.GetInt("В каком формате сохранить? Выбери цифру\n1-png\n2-jpg",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkGreen);
    }

    private static string DefineFontFamilyName(int font)
    {
        string fontFamilyName = "Arial";
        switch (font)
        {
            case 1:
                fontFamilyName = "Arial";
                break;
            case 2:
                fontFamilyName = "Times New Roman";
                break;
        }

        return fontFamilyName;
    }

    private static int GetFontFamilyName()
    {
        return Prompt.GetInt("Какой шрифт использовать? Выбери цифру\n1-Arial\n2-Times New Roman",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkCyan);
    }

    private static Brush ColorBrush(int color)
    {
        Brush colorBrush = Brushes.Purple;
        switch (color)
        {
            case 1:
                colorBrush = Brushes.Purple;
                break;
            case 2:
                colorBrush = Brushes.Green;
                break;
        }

        return colorBrush;
    }

    private static int GetColor()
    {
        return Prompt.GetInt("Какого цвета будут слова в облаке? Выбери цифру\n1-фиолетого\n2-зелёного",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkBlue);
    }

    private static bool GetIsVerticalWords()
    {
        return Prompt.GetYesNo("В облаке могут быть вертикальные слова?",
            defaultAnswer: true,
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkGreen);
    }

    private static int GetSize()
    {
        return Prompt.GetInt(
            "Изображение будет квадратное, введи размер стороны квадрата (от 2000 до 4000 пикселей)",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkCyan);
    }

    private static string? GetDirectoryToSave()
    {
        return Prompt.GetString("Введи директрорию, в которую можно сохранить изображение:",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkBlue);
    }

    private static string? GetInputWordsPath()
    {
        return Prompt.GetString(
            "Привет! Я сделаю облако тегов из набора слов :) \nРасскажи, где лежит файл со словами:",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkGreen);
    }
}