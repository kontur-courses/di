using System.Drawing;
using TagCloud.App.CloudCreatorDriver.CloudDrawers;
using TagCloud.App.CloudCreatorDriver.CloudDrawers.WordToDraw;
using TagCloud.App.CloudCreatorDriver.DrawingSettings;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters;
using TagCloud.App.WordPreprocessorDriver.InputStream;
using TagCloud.App.WordPreprocessorDriver.InputStream.TextSplitters;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.CloudCreatorDriver.CloudCreator;

public class CloudCreator : ICloudCreator
{
    public Bitmap CreatePicture(
        IInputWordsStream inputWordsStream, IWordsPreprocessor wordsPreprocessor,
        ITextSplitter textSplitter, IReadOnlyCollection<IBoringWords> boringWords,
        ICloudLayouter cloudLayouter, ICloudLayouterSettings cloudLayouterSettings,
        ICloudDrawer cloudDrawer,
        IDrawingSettings drawingSettings, IWordVisualisation defaultVisualisation)
    {
        var allWords = GetAllWordsFromStream(inputWordsStream, textSplitter);
        var words = GetProcessedWords(allWords, wordsPreprocessor, boringWords);
        var sizes = GetWordsSizes(words, defaultVisualisation, drawingSettings);

        cloudLayouter.SetSettings(cloudLayouterSettings);
        var rectangles = GetWordsRectangles(sizes, cloudLayouter);

        var drawingWords =
            CreateDrawingWords(words, rectangles, defaultVisualisation, drawingSettings.Visualisations);

        return cloudDrawer.DrawWords(drawingWords, drawingSettings);
    }

    private static IEnumerable<Size> GetWordsSizes(
        IEnumerable<IWord> words,
        IWordVisualisation defaultVisualisation,
        IDrawingSettings drawingSettings)
    {
        return words.Select(word =>
        {
            var stile = GetVisualisation(word, defaultVisualisation, drawingSettings.Visualisations);
            return GetSizeForWord(word, stile);
        });
    }

    private static List<IWord> GetProcessedWords(
        List<string> allWordsFromStream,
        IWordsPreprocessor wordsPreprocessor,
        IReadOnlyCollection<IBoringWords> boringWords)
    {
        return wordsPreprocessor.GetProcessedWords(allWordsFromStream, boringWords)
            .OrderByDescending(word => word.Tf)
            .ToList();
    }

    private static List<string> GetAllWordsFromStream(IInputWordsStream inputWordsStream, ITextSplitter textSplitter)
    {
        var allWords = new List<string>();
        inputWordsStream.UseSplitter(textSplitter);
            
        while (inputWordsStream.MoveNext())
            allWords.Add(inputWordsStream.GetWord());
        return allWords;
    }

    private static IEnumerable<Rectangle> GetWordsRectangles(IEnumerable<Size> sizes, ICloudLayouter cloudLayouter)
    {
        return sizes.Select(cloudLayouter.PutNextRectangle);
    }

    private static Size GetSizeForWord(IWord word, IWordVisualisation visualisation)
    {
        return word.MeasureWord(visualisation.Font);
    }

    private static List<IDrawingWord> CreateDrawingWords(IEnumerable<IWord> words,
        IEnumerable<Rectangle> rectangles,
        IWordVisualisation defaultVisualisation,
        IReadOnlyCollection<IWordVisualisation> wordVisualisations)
    {
        var result = new List<IDrawingWord>();
        using var enumerator = rectangles.GetEnumerator();
        foreach (var word in words)
        {
            if (!enumerator.MoveNext()) break;
            var stile = GetVisualisation(word, defaultVisualisation, wordVisualisations);
            result.Add(new DrawingWord(word, stile.Font, stile.Color, enumerator.Current));
        }

        return result;
    }

    private static IWordVisualisation GetVisualisation(IWord word,
        IWordVisualisation defaultVisualisation, IReadOnlyCollection<IWordVisualisation> wordVisualisations)
    {
        if (wordVisualisations == null)
            throw new ArgumentException("words Visualisation can not be null");

        return
            wordVisualisations.FirstOrDefault(v => v.StartingValue <= word.Tf)
            ?? defaultVisualisation;
    }
}