using System.Drawing;
using System.Text;
using McMaster.Extensions.CommandLineUtils;

namespace TagsCloud2;

public class ConsoleManager : IManager
{
    private IReader reader;
    private ILemmatizer lemmatizer;
    private IFrequencyCompiler frequencyCompiler;
    private IImageSaver imageSaver;
    private ITagsCloudMaker tagsCloudMaker;
    private BitmapMaker bitmapMaker;
    private SizeDefiner sizeDefiner;
    public ConsoleManager(IReader reader, ILemmatizer lemmatizer, IFrequencyCompiler frequencyCompiler, IImageSaver imageSaver,
        ITagsCloudMaker tagsCloudMaker, BitmapMaker bitmapMaker, SizeDefiner sizeDefiner)
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
        //
        Console.OutputEncoding = Encoding.UTF8;
        var path = Prompt.GetString(
            "Привет! Я сделаю облако тегов из набора слов :) \nРасскажи, где лежит файл со словами:",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkGreen);
        if (!File.Exists(path))
        {
            Console.WriteLine($"Файла {path} нет :( Пока!");
            return;
        }

        var pathToSave = Prompt.GetString("Введи директрорию, в которую можно сохранить изображение:",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkBlue);
        if (!Directory.Exists(pathToSave))
        {
            Console.WriteLine($"Директории {path} нет :( Пока!");
            return;
        }

        var size = Prompt.GetInt(
            "Изображение будет квадратное, введи размер стороны квадрата (от 1000 до 5000 пикселей)",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkCyan);

        if (size < 1000 && size > 5000)
        {
            Console.WriteLine("Размер не из диапазона :( Пока!");
            return;
        }

        var isVerticalWords = Prompt.GetYesNo("В облаке могут быть вертикальные слова?",
            defaultAnswer: true,
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkGreen);

        var color = Prompt.GetInt("Какого цвета будут слова в облаке? Выбери цифру\n1-фиолетого\n2-зелёного",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkBlue);

        if (color < 1 && color > 2)
        {
            Console.WriteLine("Такого цвета нет :( Пока!");
            return;
        }

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

        var font = Prompt.GetInt("Какой шрифт использовать? Выбери цифру\n1-Arial\n2-Times New Roman",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkCyan);


        if (font < 1 && font > 2)
        {
            Console.WriteLine("Такого шрифта нет :( Пока!");
            return;
        }

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


        var format = Prompt.GetInt("В каком формате сохранить? Выбери цифру\n1-png\n2-jpg",
            promptColor: ConsoleColor.White,
            promptBgColor: ConsoleColor.DarkGreen);

        if (format < 1 && format > 2)
        {
            Console.WriteLine("Такого формата нет :( Пока!");
            return;
        }
        
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
        Console.WriteLine($"Сохранено в файле {pathToSave}\\img.{formatToSave}! :)");
    }
}