using System.Drawing;
using TagsCloudContainer;
using TagsCloudContainer.WordsInterfaces;

namespace TagsCloudVisualization;

public class DrawingModel : IDrawingModel
{
    private IWordsCollector _wordsCollector;
    private ISizeManager _sizeManager;
    
    public Dictionary<string, Rectangle> WordsToSizes { get; private set; }
    public int FieldWidth { get; private set; }
    public int FieldHeight { get; private set; }
    public HashSet<Brush> Colors { get; private set; }
    public string FilePath { get; private set; }
    
    public string FontName { get; private set; }
    

    public DrawingModel(IWordsCollector wordsCollector, ISizeManager sizeManager)
    {
        _wordsCollector = wordsCollector;
        _sizeManager = sizeManager;
    }

    public DrawingModel GetDrawingModel(CommandLineOptions options)
    {
        FilePath = options.OutputFile;
        Colors = options.ColorsParsed;
        FieldHeight = options.Height;
        FieldWidth = options.Width;
        FontName = options.FontName;

        var collectedData = _wordsCollector
            .Collect(options.InputFile, options.WordsToIgnore, options.SpPartsToIgnore);

        WordsToSizes = _sizeManager
            .GetSizesForWords(new Size(options.Width, options.Height), collectedData.Item1,
                collectedData.Item2);

        return this;
    }
}