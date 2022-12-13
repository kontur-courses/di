using System.Drawing;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.CloudCreatorDriver.CloudDrawers.DrawingSettings;

public interface IWordsVisualisationSelector
{
    DrawingWord GetWordVisualisation(IWord word, Rectangle rectangle);

    void AddWordPossibleColors(IEnumerable<Color> colors);

    void SetWordsFontName(string font);

    void SetWordsSizes(int min, int max);

    void SetMinAndMaxRealWordTfIndex(double min, double max);

    bool Empty();
}