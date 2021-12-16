using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Autofac;
using CommandDotNet;
using TagsCloudContainerCore.Helpers;
using TagsCloudContainerCore.InterfacesCore;

namespace TagsCloudContainerCore.Console;

// ReSharper disable once InconsistentNaming
// ReSharper disable once ClassNeverInstantiated.Global
[SuppressMessage("Interoperability", "CA1416", MessageId = "Проверка совместимости платформы")]
public class WinConsoleUI
{
    public static void Main(string[] args)
    {
        if (!File.Exists("./TagsCloudSettings.xml"))
        {
            XmlSettingsHelper.CreateSettingsFile();
        }


        new AppRunner<WinConsoleUI>().Run(args);
    }

    [Command("font",
        Description = "Настройка шрифта для отрисовки облака",
        Usage = "%AppName% font --name <string>\"font name\" --size <int> --color <HEX>")]
    public void SetFont(
        [Named("name", Description = "Имя используемого шрифта")]
        string name = null,
        [Named("size", Description = "Максимальный размер шрифта")]
        int? maxFontSize = null,
        [Named("color", Description = "Цвет шрифта")]
        string color = null
    )
    {
        var settings = XmlSettingsHelper.GetLayoutSettings();
        if (name != null)
        {
            using var checkFont = new Font(name, 10);

            if (!checkFont.Name.Equals(name))
            {
                throw new ArgumentException($"Шрифт {name} не установлен в системе\n{checkFont.Name}");
            }

            settings.FontName = name;
            System.Console.WriteLine($"Установлен шрифт \"{name}\"\n");
        }

        if (color != null)
        {
            var colorRegex = new Regex("^[0-1a-fA-F]{6}$");
            if (!colorRegex.IsMatch(color))
            {
                throw new ArgumentException("Вы ввели не правильную кодировку цвета.\n" +
                                            " Используйте представление HEX, например \"FF01AB\"");
            }

            settings.FontColor = color.ToUpperInvariant();
            System.Console.WriteLine($"Установлен цвет шрифта: \"{color}\"\n");
        }

        if (maxFontSize != null)
        {
            if (maxFontSize is <= 0 or > 200)
            {
                throw new ArgumentException("Размер шрифта должен быть больше 0 и не превышать 200");
            }

            settings.MaxFontSize = maxFontSize.Value;
            System.Console.WriteLine($"Установлен максимальный размер шрифта: \"{settings.MaxFontSize}\"\n");
        }

        XmlSettingsHelper.CreateSettingsFile(settings);
        PrintSettings();
    }


    [Command("settings", Description = "Выводит текущие настройки")]
    // ReSharper disable once MemberCanBePrivate.Global
    public void PrintSettings()
    {
        var settings = XmlSettingsHelper.GetLayoutSettings();
        System.Console.WriteLine("\n" + settings);
    }


    [Command("reset", Description = "Сбрасывает все настройки до начальных")]
    public void ResetSettings()
    {
        XmlSettingsHelper.CreateSettingsFile();
        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine("\n[Настройки сброшены]".ToUpper());
        PrintSettings();
        System.Console.ResetColor();
    }

    [Command("circle", Description = "Настройки алгоритма")]
    public void SetAlgorithmSettings(
        [Named("angle", Description = "Минимальный угол в градусах с которым будем шагать по окружности. " +
                                      "Должен быть >0")]
        float? minAngle = null,
        [Named("step", Description = "Шаг, на который будем увеличивать радиус. Должен быть > 0")]
        int? step = null)
    {
        var settings = XmlSettingsHelper.GetLayoutSettings();

        if (minAngle != null)
        {
            if (minAngle < 0)
            {
                throw new ArgumentException("Угол должен быть положительным");
            }

            settings.MinAngle = minAngle.Value;
            System.Console.WriteLine($"Минимальный угол установлен на {minAngle.Value} градусов");
        }

        if (step != null)
        {
            if (step < 0)
            {
                throw new ArgumentException("Размер шага должен быть положительным");
            }

            settings.Step = step.Value;
            System.Console.WriteLine($"Шаг радиуса установлен на {step.Value} пикселей");
        }

        XmlSettingsHelper.CreateSettingsFile(settings);

        PrintSettings();
    }

    // ReSharper disable once StringLiteralTypo
    [Command("picsize", Description = "Устанавливает размер выходного изображения")]
    public void SetPictureSize(
        [Named("width")] int width,
        [Named("height")] int height
    )
    {
        if (width <= 0 || height <= 0)
        {
            throw new ArgumentException("Длина и ширина должны быть положительными числами");
        }

        var size = new Size(width, height);
        var settings = XmlSettingsHelper.GetLayoutSettings();

        settings.PictureSize = size;

        System.Console.WriteLine($"Установлен размер изображения {size}");

        XmlSettingsHelper.CreateSettingsFile(settings);

        PrintSettings();
    }

    [Command("background", Description = "Устанавливает цвет заднего фона")]
    public void SetBackgroundColor(
        [Named("color")] string color)
    {
        var colorRegex = new Regex("^[0-1a-fA-F]{6}$");
        if (!colorRegex.IsMatch(color))
        {
            throw new ArgumentException("Вы ввели не правильную кодировку цвета.\n" +
                                        " Используйте преставление HEX, например \"FF01AB\"");
        }

        var settings = XmlSettingsHelper.GetLayoutSettings();
        settings.BackgroundColor = color;
        XmlSettingsHelper.CreateSettingsFile(settings);
        System.Console.WriteLine($"Установлен цвет фона: \"{color}\"\n");
    }

    [Command("exclude", Description = "Добавляем файл с исключёнными словами. " +
                                      "Файл должен содержать слова записанные через пробел")]
    public void ExcludeWordsFromFile(
        [Positional(Description = "Путь до файла")]
        string path)
    {
        if (!File.Exists(path))
        {
            throw new ArgumentException($"Файл {path} не существует");
        }

        var settings = XmlSettingsHelper.GetLayoutSettings();
        settings.PathToExcludedWords = path;

        XmlSettingsHelper.CreateSettingsFile(settings);
        PrintSettings();
    }

    [Command("print",
        Description = "Печатаем облако тегов по тексту из in в файл out,\n" +
                      "если в out указать только директорию, то именем будет текущие дата и время, формат файла png.\n"+
                      "Что-бы сохранить картинку в нужном расширении в out нужно указать путь на файл с соответствующим расширением")]
    // ReSharper disable once MemberCanBePrivate.Global
    public void DrawCloud(
        [Named("in", Description = "Путь к файлу со списком тегов")]
        string pathIn,
        [Named("out", Description = "Путь к файлу, куда сохранить изображение")]
        string pathOut)
    {
        var settings = XmlSettingsHelper.GetLayoutSettings();

        var container = DICloudLayouterContainerFactory.GetContainer(settings);

        var layouter = container.Resolve<ITagCloudMaker<LayoutSettings>>();
        var painter = container.Resolve<IPainter>();

        var rawTags = FileRiderHelper.GetTags(pathIn);
        var tagsToRender = layouter.GetTagsToRender(rawTags, settings);

        using var picture = painter.Paint(tagsToRender, settings.PictureSize);
        WinSaver.Save(picture, pathOut);

        System.Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("\n[ГОТОВО]\n");
        System.Console.ResetColor();
    }
}