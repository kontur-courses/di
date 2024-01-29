using System.Drawing;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using TagsCloudCore.BuildingOptions;
using TagsCloudCore.Common;
using TagsCloudCore.Drawing.Colorers;
using TagsCloudCore.Utils;
using TagsCloudCore.WordProcessing.WordInput;

namespace TagsCloudConsoleUI.Providers;

public class ConsoleSettingsProvider : IDrawingOptionsProvider, ICommonOptionsProvider
{
    public DrawingOptions DrawingOptions => _drawingOptions ??= GetDrawingOptions();

    public CommonOptions CommonOptions => _commonOptions ??= GetCommonOptions();

    private DrawingOptions? _drawingOptions;

    private CommonOptions? _commonOptions;

    private bool _isCustomColoringUsed;

    private readonly IEnumerable<IWordColorer> _registeredWordColorers;
    public ConsoleSettingsProvider(IEnumerable<IWordColorer> registeredWordColorers)
    {
        _registeredWordColorers = registeredWordColorers;
    }
    
    private DrawingOptions GetDrawingOptions()
    {
        var font = GetFont();
        var backgroundColor = GetRgbColor("Enter background color in RGB format separated by space");

        var fontColor = GetFontColor();
        
        var imageSide = GetInteger(
            "Enter the image's desired size in px. The image will be a square. It must range from 500 px to 5000 px.",
            500, 5000);
        
        var imageSize = new Size(imageSide, imageSide);
        
        var frequencyScaling = GetInteger(
                "Enter the frequency scaling (a positive integer). It Determines the scale to word frequency ratio.", 
                1, 100);

        return new DrawingOptions(fontColor, backgroundColor, imageSize, font, frequencyScaling);
    }

    private CommonOptions GetCommonOptions()
    {
        var wordProvider = GetWordProvider();
        var wordColorer = GetWordColorer();
        var cloudLayouter = GetAlgorithm(CloudAlgorithmProvider.RegisteredProviders,
            "Choose the cloud forming algorithm:");
        
        return new CommonOptions(wordProvider, wordColorer, cloudLayouter);
    }

    private Color GetFontColor()
    {
        return _isCustomColoringUsed 
            ? Color.White 
            : GetRgbColor("Enter font color in RGB format separated by space");
    }
    
    private static int GetInteger(string prompt, int lowerAllowedBoundary, int upperAllowedBoundary)
    {
        while (true)
        {
            var intString = Prompt.GetString(prompt, "", ConsoleColor.DarkGreen);
            if (int.TryParse(intString, out var parsed) && parsed >= lowerAllowedBoundary &&
                parsed <= upperAllowedBoundary)
                return parsed;
            Console.WriteLine("Given number is invalid. Try again.");
        }
    }

    private static Color GetRgbColor(string prompt)
    {
        while (true)
        {
            var colorString = Prompt.GetString(prompt, "",
                promptColor: ConsoleColor.DarkGreen);
            if (DrawingUtils.TryParseRgb(colorString!, out var color))
                return color;
            Console.WriteLine("Ivalid color format. Try again.");
        }
    }

    private static Font GetFont()
    {
        var fontName = Prompt.GetString("Enter font name", promptColor: ConsoleColor.DarkGreen) ??
                       "Microsoft Sans Serif";
        while (true)
        {
            var fontSizeStr = Prompt.GetString("Enter font size in pt", promptColor: ConsoleColor.DarkGreen);
            if (float.TryParse(fontSizeStr, out var fontSize) && fontSize > 0)
                return new Font(fontName, fontSize);
            Console.WriteLine("Font size must be a correct positive number. Try again.");
        }
    }

    private IWordColorer GetWordColorer()
    {
        var sb = new StringBuilder("Choose coloring algorithm:\n");
        foreach (var registeredWordColorer in _registeredWordColorers)
            sb.AppendLine(registeredWordColorer.Name);

        while (true)
        {
            var input = Prompt.GetString(sb.ToString(), "", promptColor: ConsoleColor.DarkGreen);
            var colorer = _registeredWordColorers.SingleOrDefault(c => c.Match(input!));
            if (colorer is null)
            {
                Console.WriteLine("The provided algorithm does not exist. Try again.");
                continue;
            }

            if (colorer.Name != AppSettings.DefaultColorerName)
                _isCustomColoringUsed = true;
            return colorer;
        }
    }

    private static T GetAlgorithm<T>(IReadOnlyDictionary<string, T> registeredAlgorithms,
        string prompt)
    {
        var sb = new StringBuilder($"{prompt}\n");
        foreach (var algorithmProvider in registeredAlgorithms.Keys)
            sb.AppendLine(algorithmProvider);

        while (true)
        {
            var algorithm = Prompt.GetString(sb.ToString(), "", promptColor: ConsoleColor.DarkGreen);
            if (registeredAlgorithms.TryGetValue(algorithm!, out var provider))
                return provider;

            Console.WriteLine("Specified algorithm isn't supported. Try again.");
        }
    }

    private static IWordProvider GetWordProvider()
    {
        while (true)
        {
            var path = Prompt.GetString("Enter the path to the file with words", promptColor: ConsoleColor.DarkGreen);
            if (!File.Exists(path))
            {
                Console.WriteLine("Provided path is invalid. Try again.");
                continue;
            }

            var ext = Path.GetExtension(path);
            if (WordProviders.RegisteredProviders.TryGetValue(ext, out var provider))
                return provider(path);

            Console.WriteLine("This extension is not supported. Try again.");
        }
    }
}