using System.Drawing;
using TagCloud.App.CloudCreatorDriver.CloudCreator;
using TagCloud.App.CloudCreatorDriver.CloudDrawers.DrawingSettings;
using TagCloud.App.CloudCreatorDriver.ImageSaver;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters.SpiralCloudLayouters;
using TagCloud.App.WordPreprocessorDriver.InputStream;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;

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
    private readonly FromFileInputWordsStream inputWordsStream;
        
    public ConsoleClient(
        IReadOnlyCollection<IFileEncoder> fileEncoders,
        ICloudLayouterSettings cloudLayouterSettings,
        IDrawingSettings drawingSettings,
        ICloudCreator cloudCreator,
        IImageSaver imageSaver, FromFileInputWordsStream inputWordsStream)
    {
        this.fileEncoders = fileEncoders;
        this.cloudLayouterSettings = cloudLayouterSettings;
        this.drawingSettings = drawingSettings;
        creator = cloudCreator;
        this.imageSaver = imageSaver;
        this.inputWordsStream = inputWordsStream;
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
                if (!TryGetFileEncoder(fileEncoders, path, out var suitableFileEncoder))
                {
                    Console.WriteLine(Phrases.FailGettingFileEncoder);
                    return;
                }

                var streamContext = new FromFileStreamContext(path, suitableFileEncoder!);
                drawingSettings.BgColor = bgColor;
                drawingSettings.PictureSize = imageSize;
                ((SpiralCloudLayouterSettings)cloudLayouterSettings).Center =
                    new Point(imageSize.Width / 2, imageSize.Height / 2);

                Console.Write(Phrases.AskingAddingWordVisualisationRule);
                if (Console.ReadLine() == Phrases.Yes) 
                    FillWordVisualisationSettings(drawingSettings);
                
                Console.Write(Phrases.AskingAddingUsersBoringWords);
                if (Console.ReadLine() == Phrases.Yes)
                    creator.AddBoringWordManager(GetBoringWords());
                
                var image = creator.CreatePicture(streamContext);

                Console.WriteLine(imageSaver.TrySaveImage(image, savePath!)
                    ? Phrases.SuccessSaveImage + savePath
                    : Phrases.FailImageSaving);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(Phrases.UnexpectedError(e));
        }

        Stop();
    }

    private BoringWordsFromUser GetBoringWords()
    {
        Console.WriteLine(Phrases.AskingFullPathToBoringWords);
        
        var boringWords = new BoringWordsFromUser();
        if (!TryGetFilePathFromConsole(out var boringWordsPath) || boringWordsPath == null)
        {
            Console.Write(Phrases.FailGettingFullPath + Phrases.TryAgain);
            return Console.ReadLine() == Phrases.Yes
                ? GetBoringWords()
                : boringWords;

        }
        
        if (!TryGetFileEncoder(fileEncoders, boringWordsPath!, out var suitableFileEncoder))
        {
            Console.WriteLine(Phrases.FailGettingFileEncoder);
            return boringWords;
        }
        
        var streamContext = new FromFileStreamContext(boringWordsPath!, suitableFileEncoder!);
        foreach (var word in inputWordsStream.GetAllWordsFromStream(streamContext))
        {
            boringWords.AddBoringWord(word);
        }
        Console.WriteLine(Phrases.SuccessUploadBoringWords);

        return boringWords;
    }

    private static void FillWordVisualisationSettings(IDrawingSettings drawingSettings)
    {
        Console.WriteLine(Phrases.StartCreatingNewWordVisualisation);
        if (!TryGetDoubleFromConsole(Phrases.AskingWordImportance, out var startValue)
            || !TryGetWordsColor(out var color)
            || !TryGetIntFromConsole(Phrases.AskingFontSize, out var fontSize)
            || !TryGetFont(Phrases.AskingFontName, out var font, fontSize))
            return;
        
        drawingSettings.AddWordVisualisation(new WordVisualisation(color, startValue, font!));
        
        Console.WriteLine(Phrases.EndCreatingNewWordVisualisation);
        Console.Write(Phrases.AskingAddingWordVisualisationRule);
        if (Console.ReadLine() == Phrases.Yes)
            FillWordVisualisationSettings(drawingSettings);
    }

    private static bool TryGetFont(string text, out Font? font, int fontSize)
    {
        Console.Write(text);
        
        var value = Console.ReadLine();
        if (value != null)
        {
            font = new Font(value, fontSize);
            return true;
        }

        font = null;
        Console.Write(Phrases.FailGettingFont + Phrases.TryAgain);
        return Console.ReadLine() != Phrases.Yes && TryGetFont(text, out font, fontSize);
    }
        
    private static bool TryGetIntFromConsole(string text, out int result)
    {
        Console.Write(text);
        
        var value = Console.ReadLine();
        if (int.TryParse(value, out result))
            return true;
        
        Console.Write(Phrases.FailGettingIntValue + Phrases.TryAgain);
        return Console.ReadLine() != Phrases.Yes && TryGetIntFromConsole(text, out result);
    }

    private static bool TryGetDoubleFromConsole(string text, out double result)
    {
        Console.Write(text);
        var value = Console.ReadLine();
        if (double.TryParse(value, out result))
            return true;
        Console.Write(Phrases.FailGettingDoubleValue + Phrases.TryAgain);
        return Console.ReadLine() == Phrases.Yes && TryGetDoubleFromConsole(text, out result);
    }

    private static bool TryGetFileEncoder(
        IEnumerable<IFileEncoder> fileEncoders,
        string fileName,
        out IFileEncoder? fileEncoder)
    {
        fileEncoder = fileEncoders.FirstOrDefault(encoder =>
            fileName.EndsWith(encoder.GetExpectedFileType()));
        return fileEncoder != null;
    }

    private static void Start()
    {
        Console.WriteLine(Phrases.Hello);
    }
        
    private static bool TryGetWordsColor(out Color color)
    {
        Console.Write(Phrases.AskingWordColor);

        if (TryGetColorFromConsole(out color))
            return true;
            
        Console.Write(Phrases.FailGettingColor + Phrases.TryAgain);
        return Console.ReadLine() == Phrases.Yes && TryGetWordsColor(out color);
    }

    private static bool TryGetBgColor(out Color color)
    {
        Console.Write(Phrases.AskingBgColor);

        if (TryGetColorFromConsole(out color))
            return true;
            
        Console.Write(Phrases.FailGettingColor + Phrases.TryAgain);
        return Console.ReadLine() == Phrases.Yes && TryGetBgColor(out color);
    }

    private static bool TryGetColorFromConsole(out Color color)
    {
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
        Console.Write(Phrases.AskingImageSize);

        if (TryGetImageSizeFromConsole(out size))
            return true;
            
        Console.Write(Phrases.FailGettingImageSize + Phrases.TryAgain);
        return Console.ReadLine() == Phrases.Yes && TryGetImageSize(out size);
    }

    private static bool TryGetImageSizeFromConsole(out Size size)
    {
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
        Console.WriteLine(Phrases.AskingFullPathToOutImage);
        Console.Write(Phrases.GetArrow(0));
        outPath = Console.ReadLine();
        return outPath != null;
    }
        
    private static bool TryGetFilePath(out string filePath)
    {
        Console.WriteLine(Phrases.AskingFullPathToText);
        
        if (TryGetFilePathFromConsole(out var path) && path != null)
        {
            filePath = path;
            return true;
        }
            
        Console.Write(Phrases.FailGettingFullPath + Phrases.TryAgain);
        if (Console.ReadLine() == Phrases.Yes) return TryGetFilePath(out filePath);
        filePath = "";
        return false;
    }

    private static bool TryGetFilePathFromConsole(out string? filePath)
    {
        Console.Write(Phrases.GetArrow(0));
        var path = Console.ReadLine();
        filePath = path;
        return File.Exists(path);
    }

    private static void Stop()
    {
        Console.WriteLine(Phrases.GoodBy);
    }
}