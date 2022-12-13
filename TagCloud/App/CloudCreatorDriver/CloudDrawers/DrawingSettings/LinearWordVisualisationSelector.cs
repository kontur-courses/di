using System.Drawing;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.CloudCreatorDriver.CloudDrawers.DrawingSettings;

public class LinearWordVisualisationSelector : IWordsVisualisationSelector
{
    private readonly List<Color> possibleColors = new();
    private int minSize;
    private int maxSize;
    // ReSharper disable once RedundantDefaultMemberInitializer
    private double minTf = 0;
    private double maxTf = 1;
    private string wordsFont;


    public LinearWordVisualisationSelector(string wordsFont, int minSize, int maxSize)
    {
        this.wordsFont = wordsFont;
        SetWordsSizes(minSize, maxSize);
    }

    public DrawingWord GetWordVisualisation(IWord word, Rectangle rectangle)
    {
        if (possibleColors.Count == 0)
            throw new Exception("Possible colors are not initialised");
        var fontDelta = maxSize - minSize;
        var tfDelta = maxTf - minTf;
        var size = minSize + (int)Math.Floor(fontDelta * (word.Tf-minTf) / tfDelta) ;
        var colorIdx = (int)Math.Floor(1d * (possibleColors.Count-1) * (word.Tf-minTf) / tfDelta);
        return new DrawingWord(word, new Font(wordsFont, size), possibleColors[colorIdx], rectangle);
    }

    public void AddWordPossibleColors(IEnumerable<Color> colors)
    {
        possibleColors.AddRange(colors);
    }

    public void SetWordsFontName(string font)
    {
        wordsFont = font;
    }

    public void SetWordsSizes(int min, int max)
    {
        if (min > max)
            throw new ArgumentException($"Min size {min} should be less or equal max size {max}");
        if (min <= 0)
            throw new ArgumentException($"Min value {min} should be positive");
        minSize = min;
        maxSize = max;
    }

    public void SetMinAndMaxRealWordTfIndex(double min, double max)
    {
        if (min > max)
            throw new ArgumentException($"Min size {min} should be less or equal max size {max}");
        minTf = min;
        maxTf = max;
    }

    public bool Empty()
    {
        return possibleColors.Count == 0;
    }
}