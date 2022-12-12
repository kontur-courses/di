using System.Drawing;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using TagsCloud2.FrequencyCompiler;
using TagsCloud2.ImageSaver;
using TagsCloud2.Lemmatizer;
using TagsCloud2.Reader;
using TagsCloud2.TagsCloudMaker;
using TagsCloud2.TagsCloudMaker.BitmapMaker;
using TagsCloud2.TagsCloudMaker.SizeDefiner;

namespace TagsCloud2.Manager;

public class ConsoleManager : IManager
{
    private IReader reader;
    private ILemmatizer lemmatizer;
    private IFrequencyCompiler frequencyCompiler;
    private IImageSaver imageSaver;
    private ITagsCloudMaker tagsCloudMaker;
    private IBitmapMaker bitmapMaker;
    private ISizeDefiner sizeDefiner;
    public ConsoleManager(IReader reader, ILemmatizer lemmatizer, IFrequencyCompiler frequencyCompiler, IImageSaver imageSaver,
        ITagsCloudMaker tagsCloudMaker, IBitmapMaker bitmapMaker, ISizeDefiner sizeDefiner)
    {
        this.reader = reader;
        this.lemmatizer = lemmatizer;
        this.frequencyCompiler = frequencyCompiler;
        this.imageSaver = imageSaver;
        this.tagsCloudMaker = tagsCloudMaker;
        this.bitmapMaker = bitmapMaker;
        this.sizeDefiner = sizeDefiner;
    }
    public void Manage()
    {
        Console.OutputEncoding = Encoding.UTF8;
        var path = GetInputWordsPath();
        if (!File.Exists(path))
        {
            Console.WriteLine($"Файла {path} нет :( Пока!");
            return;
        }
        
        var pathToSave = GetDirectoryToSave();
        if (!Directory.Exists(pathToSave))
        {
            Console.WriteLine($"Директории {path} нет :( Пока!");
            return;
        }
        
        var size = GetSize();
        if (size < 2000 && size > 4000)
        {
            Console.WriteLine("Размер не из диапазона :( Пока!");
            return;
        }

        var isVerticalWords = GetIsVerticalWords();

        var color = GetColor();
        if (color < 1 && color > 2)
        {
            Console.WriteLine("Такого цвета нет :( Пока!");
            return;
        }
        var colorBrush = ColorBrush(color);

        var font = GetFontFamilyName();
        if (font < 1 && font > 2)
        {
            Console.WriteLine("Такого шрифта нет :( Пока!");
            return;
        }
        var fontFamilyName = DefineFontFamilyName(font);
        
        var format = GetFormatToSave();

        if (format < 1 && format > 2)
        {
            Console.WriteLine("Такого формата нет :( Пока!");
            return;
        }

        var formatToSave = DefineFormatToSave(format);

        var words = reader.ReadWordsFromFile(path);
        Console.WriteLine("Ждите..");
        var normalizeWords = lemmatizer.Lemmatize(words);
        Console.WriteLine("Ждите..");
        var frequencyDict = frequencyCompiler.GetFrequencyOfWords(normalizeWords);
        Console.WriteLine("Ждите..");
        var frequencyList = frequencyCompiler.GetFrequencyList(frequencyDict, 100);
        Console.WriteLine("Ждите..");
        var tagsCloudBitmap = tagsCloudMaker.MakeTagsCloud(frequencyList, fontFamilyName, 50,
            colorBrush, new Size(size, size), bitmapMaker, sizeDefiner, isVerticalWords);
        Console.WriteLine("Ждите..");
        imageSaver.SaveImage(pathToSave, @"img", formatToSave, tagsCloudBitmap);
        Console.WriteLine($"Сохранено в файле {pathToSave}+img.{formatToSave}! :)");
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