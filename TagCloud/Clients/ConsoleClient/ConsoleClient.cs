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
    private readonly IWordsVisualisationSelector wordsVisualisationSelector;

    public ConsoleClient(
        IReadOnlyCollection<IFileEncoder> fileEncoders,
        ICloudLayouterSettings cloudLayouterSettings,
        IDrawingSettings drawingSettings,
        ICloudCreator cloudCreator,
        IImageSaver imageSaver, FromFileInputWordsStream inputWordsStream,
        IWordsVisualisationSelector wordsVisualisationSelector)
    {
        this.fileEncoders = fileEncoders;
        this.cloudLayouterSettings = cloudLayouterSettings;
        this.drawingSettings = drawingSettings;
        creator = cloudCreator;
        this.imageSaver = imageSaver;
        this.inputWordsStream = inputWordsStream;
        this.wordsVisualisationSelector = wordsVisualisationSelector;
    }

    public void Run()
    {
        Start();

        try
        {
            if (!TryGetFilePath(out path)
                || !TryGetImageSize(out imageSize)
                || !TryGetBgColor(out bgColor)
                || !TryGetOutImagePath(out savePath))
                return;

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

            Console.Write(Phrases.AskingAddingUsersBoringWords);
            if (Console.ReadLine() == Phrases.Yes)
                creator.AddBoringWordManager(GetBoringWords());

            if (!TryCollectWordsColors(wordsVisualisationSelector)
                || !TryCollectFontSizes(wordsVisualisationSelector))
                return;

            var image = creator.CreatePicture(streamContext);

            Console.WriteLine(imageSaver.TrySaveImage(image, savePath!)
                ? Phrases.SuccessSaveImage + savePath
                : Phrases.FailImageSaving);

        }
        catch (Exception e)
        {
            Console.WriteLine(Phrases.UnexpectedError(e));
        }

        Stop();
    }

    private static bool TryCollectFontSizes(IWordsVisualisationSelector selector)
    {
        Console.Write(Phrases.AskingFontSize);
        try
        {
            var sizes = Console.ReadLine()!
                .Split(' ')
                .Select(int.Parse)
                .Take(2).ToArray();
            selector.SetWordsSizes(sizes[0], sizes[1]);
            return true;
        }
        catch
        {
            Console.Write(Phrases.FailGettingFontSize + Phrases.TryAgain);
            return Console.ReadLine() == Phrases.Yes && TryCollectFontSizes(selector);
        }
    }

    private static bool TryCollectWordsColors(IWordsVisualisationSelector visualisationSelector)
    {
        Console.Write(Phrases.AskingWordsColors);
        var colors = Console.ReadLine()!
            .Split('-')
            .Select(Color.FromName)
            .Where(cColor => cColor.IsKnownColor)
            .ToArray();
        if (colors.Length > 0)
        {
            visualisationSelector.AddWordPossibleColors(colors);
            return true;
        }
        Console.Write(Phrases.FailGettingWordsColors + Phrases.TryAgain);
        return Console.ReadLine() == Phrases.Yes && TryCollectWordsColors(visualisationSelector);
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
        
        if (!TryGetFileEncoder(fileEncoders, boringWordsPath, out var suitableFileEncoder))
        {
            Console.WriteLine(Phrases.FailGettingFileEncoder);
            return boringWords;
        }
        
        var streamContext = new FromFileStreamContext(boringWordsPath, suitableFileEncoder!);
        foreach (var word in inputWordsStream.GetAllWordsFromStream(streamContext))
        {
            boringWords.AddBoringWord(word);
        }
        Console.WriteLine(Phrases.SuccessUploadBoringWords);

        return boringWords;
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