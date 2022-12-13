using System.Drawing;
using TagCloud.App.CloudCreatorDriver.CloudDrawers;
using TagCloud.App.CloudCreatorDriver.CloudDrawers.DrawingSettings;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters;
using TagCloud.App.WordPreprocessorDriver.InputStream;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.CloudCreatorDriver.CloudCreator;

public class CloudCreator : ICloudCreator
{
    private readonly FromFileInputWordsStream inputWordsStream;
    private readonly IWordsPreprocessor wordsPreprocessor;
    private readonly List<IBoringWords> boringWords;
    private readonly ICloudLayouter cloudLayouter;
    private readonly ICloudLayouterSettings cloudLayouterSettings;
    private readonly ICloudDrawer cloudDrawer;
    private readonly IDrawingSettings drawingSettings;

    public CloudCreator(
        FromFileInputWordsStream inputWordsStream,
        IWordsPreprocessor wordsPreprocessor, IEnumerable<IBoringWords> boringWords,
        ICloudLayouter cloudLayouter, ICloudLayouterSettings cloudLayouterSettings,
        ICloudDrawer cloudDrawer, IDrawingSettings drawingSettings)
    {
        this.inputWordsStream = inputWordsStream;
        this.wordsPreprocessor = wordsPreprocessor;
        this.boringWords = boringWords.ToList();
        this.cloudLayouter = cloudLayouter;
        this.cloudLayouterSettings = cloudLayouterSettings;
        this.cloudDrawer = cloudDrawer;
        this.drawingSettings = drawingSettings;
    }

    public void AddBoringWordManager(IBoringWords boringWordsManager)
    {
        boringWords.Add(boringWordsManager);
    }
    
    public Bitmap CreatePicture(FromFileStreamContext streamContext)
    {
        if (streamContext == null) throw new ArgumentNullException(nameof(streamContext));
        
        var allWords = inputWordsStream.GetAllWordsFromStream(streamContext);
        var words = GetProcessedWordsOrderedByTf(allWords, wordsPreprocessor, boringWords);

        if (drawingSettings.HasWordVisualisationSelector())
        {
            var selector = drawingSettings.GetSelector();
            var minTf = words.Min(word => word.Tf);
            var maxTf = words.Max(word => word.Tf);
            selector.SetMinAndMaxRealWordTfIndex(minTf, maxTf);
        }
        
        var sizes = GetWordsSizes(words, drawingSettings);
        var rectangles = cloudLayouter.GetLaidRectangles(sizes, cloudLayouterSettings);
        var drawingWords = CreateDrawingWords(words, rectangles, drawingSettings);

        return cloudDrawer.DrawWords(drawingWords, drawingSettings);
    }

    private static IEnumerable<Size> GetWordsSizes(
        IEnumerable<IWord> words,
        IDrawingSettings drawingSettings)
    {
        return drawingSettings.HasWordVisualisationSelector()
            ? words.Select(word => word.MeasureWord(
                drawingSettings.GetDrawingWordFromSelector(word, Rectangle.Empty).Font))
            : words.Select(word => word.MeasureWord(GetVisualisation(word, drawingSettings).Font));
    }

    private static List<IWord> GetProcessedWordsOrderedByTf(
        List<string> allWordsFromStream,
        IWordsPreprocessor wordsPreprocessor,
        IReadOnlyCollection<IBoringWords> boringWords)
    {
        return wordsPreprocessor.GetProcessedWords(allWordsFromStream, boringWords)
            .OrderByDescending(word => word.Tf)
            .ToList();
    }

    private static List<IDrawingWord> CreateDrawingWords(IEnumerable<IWord> words,
        IEnumerable<Rectangle> rectangles,
        IDrawingSettings drawingSettings)
    {
        var result = new List<IDrawingWord>();
        using var enumerator = rectangles.GetEnumerator();
        foreach (var word in words)
        {
            if (!enumerator.MoveNext()) break;
            IDrawingWord drawingWord;
            if (drawingSettings.HasWordVisualisationSelector())
                drawingWord = drawingSettings.GetDrawingWordFromSelector(word, enumerator.Current);
            else
            {
                var stile = GetVisualisation(word, drawingSettings);
                drawingWord = new DrawingWord(word, stile.Font, stile.Color, enumerator.Current);
            }
            result.Add(drawingWord);
        }

        return result;
    }

    private static IWordVisualisation GetVisualisation(IWord word,IDrawingSettings drawingSettings)
    {
        if (word == null) throw new ArgumentNullException(nameof(word));
        if (drawingSettings == null) throw new ArgumentNullException(nameof(drawingSettings));

        return
            drawingSettings
                .GetWordVisualisations()
                .OrderByDescending(visualisation => visualisation.StartingValue)
                .FirstOrDefault(v => v.StartingValue <= word.Tf)
            ?? drawingSettings.GetDefaultVisualisation();
    }
}