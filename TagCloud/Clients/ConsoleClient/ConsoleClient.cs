using System.Drawing;
using TagCloud.App.CloudCreatorDriver.CloudCreator;
using TagCloud.App.CloudCreatorDriver.DrawingSettings;
using TagCloud.App.CloudCreatorDriver.ImageSaver;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters.SpiralCloudLayouters;
using TagCloud.App.WordPreprocessorDriver.InputStream.FileInputStream;

namespace TagCloud.Clients.ConsoleClient;

public class ConsoleClient : IClient
{
    private string? path;
    private string? savePath;
    private Size imageSize;
    private Color bgColor;

    private readonly ICloudCreator creator;
    private readonly IReadOnlyCollection<IFileEncoder> fileEncoders;
    private readonly ICloudLayouterSettings cloudLayouterSettings;
    private readonly IDrawingSettings drawingSettings;
    private readonly IImageSaver imageSaver;
        
    public ConsoleClient(
        IReadOnlyCollection<IFileEncoder> fileEncoders,
        ICloudLayouterSettings cloudLayouterSettings,
        IDrawingSettings drawingSettings,
        ICloudCreator cloudCreator,
        IImageSaver imageSaver)
    {
        this.fileEncoders = fileEncoders;
        this.cloudLayouterSettings = cloudLayouterSettings;
        this.drawingSettings = drawingSettings;
        creator = cloudCreator;
        this.imageSaver = imageSaver;
    }

    public void Run()
    {
        Start();

        try
        {
            if (TryGetFilePath(out path)
                && TryGetImageSize(out imageSize)
                && TryGetBgColor(out bgColor)
                && TryGetOutImagePath(out savePath))
            {
                if (!TryGetFileEncoder(fileEncoders, new FullFileName(path), out var suitableFileEncoder))
                {
                    Console.WriteLine("Не удалось обнаружить подходящий обработчик файла данных. " +
                                      "Попробуйте сменить тип файла и запустить программу заново.");
                    return;
                }

                var streamContext = new FromFileStreamContext(path, suitableFileEncoder!);
                drawingSettings.BgColor = bgColor;
                drawingSettings.PictureSize = imageSize;
                ((SpiralCloudLayouterSettings)cloudLayouterSettings).Center =
                    new Point(imageSize.Width / 2, imageSize.Height / 2);

                Console.WriteLine();
                Console.WriteLine("Хотите задать настройки для рисования слов? [y/n]");
                Console.Write("> ");
                if (Console.ReadLine() == "y") 
                    FillWordVisualisationSettings(drawingSettings);
                
                var image = creator.CreatePicture(streamContext);

                Console.WriteLine(imageSaver.TrySaveImage(image, new FullFileName(savePath!))
                    ? $"Файл сохранён успешно в {savePath}"
                    : "Произошла ошибка при сохранении изображения. Запустите программу ещё раз");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Произошла непредвиденная ошибка " + Environment.NewLine +
                              e + Environment.NewLine +
                              "Попробуйте перезапустить программу");
        }

        Stop();
    }

    private static void FillWordVisualisationSettings(IDrawingSettings drawingSettings)
    {
        Console.WriteLine();
        Console.WriteLine("Создание нового правила оформления слов:");
        if (!TryGetDoubleFromConsole("    Значение важности слова в тексте, с корого начинается данное оформление." +
                                     Environment.NewLine + 
                                     "    Дробное число от 0 до 1 ( 0 - совсем не важно, 1 - самое важное): ",
                out var startValue))
            return;
        if (!TryGetWordsColor(out var color))
            return;
        if (!TryGetIntFromConsole("    Размер шрифта: ", out var fontSize))
            return;
        if(!TryGetFont("    Название стиля шрфта на английском: ", out var font, fontSize))
            return;
        drawingSettings.AddWordVisualisation(new WordVisualisation(color, startValue, font));
        Console.WriteLine("    Добавить ещё одно правило? [y/n]: ");
        Console.Write("    > ");
        if (Console.ReadLine() == "y")
            FillWordVisualisationSettings(drawingSettings);
    }

    private static bool TryGetFont(string text, out Font? font, int fontSize)
    {
        Console.WriteLine(text);
        Console.Write("    > ");
        var value = Console.ReadLine();
        if (value != null)
        {
            font = new Font(value, fontSize);
            return true;
        }

        font = null;
        Console.WriteLine("[ERROR] Неверно введено название шрифтв." + Environment.NewLine +
                          "    Попробовать ещё раз? [y/n]: ");
        Console.Write("    > ");
        return Console.ReadLine() != "y" && TryGetFont(text, out font, fontSize);
    }
        
    private static bool TryGetIntFromConsole(string text, out int result)
    {
        Console.WriteLine(text);
        Console.Write("    > ");
        var value = Console.ReadLine();
        if (int.TryParse(value, out result))
            return true;
        Console.WriteLine("[ERROR] Неверно введено число. Введите целое число." + Environment.NewLine +
                          "    Попробовать ещё раз? [y/n]: ");
        Console.Write("    > ");
        return Console.ReadLine() != "y" && TryGetIntFromConsole(text, out result);
    }

    private static bool TryGetDoubleFromConsole(string text, out double result)
    {
        Console.WriteLine(text);
        Console.Write("    > ");
        var value = Console.ReadLine();
        if (double.TryParse(value, out result))
            return true;
        Console.WriteLine("[ERROR] Неверно введено число. Введите десятичное число через запятую." + Environment.NewLine +
                          "    Попробовать ещё раз? [y/n]: ");
        Console.Write("    > ");
        return Console.ReadLine() == "y" && TryGetDoubleFromConsole(text, out result);
    }

    private static bool TryGetFileEncoder(
        IEnumerable<IFileEncoder> fileEncoders,
        FullFileName fileName,
        out IFileEncoder? fileEncoder)
    {
        fileEncoder = fileEncoders.FirstOrDefault(encoder =>
            fileName.Path.EndsWith(encoder.GetExpectedFileType()));
        return fileEncoder != null;
    }

    private static void Start()
    {
        Console.WriteLine("Вы запустили создателя облаков тегов." + Environment.NewLine +
                          "Я умею получать слова из файла, в котором они все записаны в столбик");
    }
        
    private static bool TryGetWordsColor(out Color color)
    {
        Console.WriteLine("    Цвет слов на английском" + Environment.NewLine +
                          "    Для выбора настроек по умолчанию введите black: ");

        if (TryGetColorFromConsole(4, out color))
            return true;
            
        Console.WriteLine("    Заданный цвет не определён. Повторите попытку? [y/n]: ");
        Console.Write("    > ");
        return Console.ReadLine() == "y" && TryGetWordsColor(out color);
    }

    private static bool TryGetBgColor(out Color color)
    {
        Console.WriteLine();
        Console.WriteLine("Пожалуйста, введите цвет фона на английском" + Environment.NewLine +
                          "Для выбора настроек по умолчанию введите white: ");

        if (TryGetColorFromConsole(0, out color))
            return true;
            
        Console.WriteLine("    Заданный цвет не определён. Повторите попытку? [y/n]: ");
        return Console.ReadLine() == "y" && TryGetBgColor(out color);
    }

    private static bool TryGetColorFromConsole(int indent, out Color color)
    {
        Console.Write(new string(' ', indent) + "> ");
        var stringColor = Console.ReadLine();
        if (stringColor == null)
        {
            color = Color.Empty;
            return false;
        }

        color = Color.FromName(stringColor);
        return color.IsKnownColor;
    }

    private static bool TryGetImageSize(out Size size)
    {
        Console.WriteLine();
        Console.WriteLine("Пожалуйста, введите размеры изображения, которое хотите получить Ш*В (в пикселях)" +
                          Environment.NewLine +
                          "Для выбора настроек по умолчанию введите 800*500: ");

        if (TryGetImageSizeFromConsole(out size))
            return true;
            
        Console.WriteLine("    Неверный формат записи размеров. Повторите попытку? [y/n]: ");
        Console.Write("    > ");
        return Console.ReadLine() == "y" && TryGetImageSize(out size);
    }

    private static bool TryGetImageSizeFromConsole(out Size size)
    {
        Console.Write("> ");
        var stringSize = Console.ReadLine();
        if (stringSize == null)
        {
            size = new Size(0, 0);
            return false;
        }

        int[] intSize;
        try
        {
            intSize = stringSize.Split('*').Select(int.Parse).ToArray();
        }
        catch
        {
            size = new Size(0, 0);
            return false;
        }

        if (intSize.Length != 2)
        {
            size = new Size(0, 0);
            return false;
        }

        size = new Size(intSize[0], intSize[1]);
        return true;
    }

    private static bool TryGetOutImagePath(out string? outPath)
    {
        Console.WriteLine();
        Console.WriteLine("Пожалуйста, введите полный путь к файлу, в который необходимо сохранить изображение: ");
        Console.Write("> ");
        outPath = Console.ReadLine();
        return outPath != null;
    }
        
    private static bool TryGetFilePath(out string filePath)
    {
        Console.WriteLine();
        Console.WriteLine("Пожалуйста, введите полный путь к вашему файлу со словами");
        Console.Write("> ");
        if (TryGetFilePathFromConsole(out var path) && path != null)
        {
            filePath = path;
            return true;
        }
            
        Console.WriteLine("    Мне не удалось обнаружить указанный файл. Повторите попытку? [y/n]: ");
        Console.Write("    > ");
        if (Console.ReadLine() == "y") return TryGetFilePath(out filePath);
        filePath = "";
        return false;
    }

    private static bool TryGetFilePathFromConsole(out string? filePath)
    {
        var path = Console.ReadLine();
        filePath = path;
        return File.Exists(path);
    }

    private static void Stop()
    {
        Console.WriteLine();
        Console.WriteLine("Спасибо за использование этой программы" +
                          Environment.NewLine + "До свидания!");
    }
}