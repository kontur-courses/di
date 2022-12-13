using System.Drawing;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.CloudCreatorDriver.CloudDrawers.DrawingSettings;

public class DrawingSettings : IDrawingSettings
{
    public Color BgColor { get; set; }
    public Size PictureSize { get; set; }
    
    private readonly IWordVisualisation defaultVisualisation;
    private IWordsVisualisationSelector visualisationSelector;
    private readonly List<IWordVisualisation> wordVisualisations;

    public DrawingSettings(IWordVisualisation defaultVisualisation, IWordsVisualisationSelector visualisationSelector)
    {
        this.defaultVisualisation = defaultVisualisation;
        this.visualisationSelector = visualisationSelector;
        wordVisualisations = new List<IWordVisualisation>(){defaultVisualisation};
    }

    public bool HasWordVisualisationSelector() => !visualisationSelector.Empty();
    public IWordsVisualisationSelector GetSelector()
    {
        return HasWordVisualisationSelector()
            ? visualisationSelector
            : throw new Exception("Selector was not initialised");
    }

    public IDrawingWord GetDrawingWordFromSelector(IWord word, Rectangle rectangle)
    {
        if (!HasWordVisualisationSelector())
            throw new Exception("Selector was not initialised");
        return visualisationSelector!.GetWordVisualisation(word, rectangle);
    }

    public void AddWordVisualisation(IWordVisualisation wordVisualisation)
    {
        wordVisualisations.Add(wordVisualisation);
    }

    public IEnumerable<IWordVisualisation> GetWordVisualisations()
    {
        return wordVisualisations;
    }

    public IWordVisualisation GetDefaultVisualisation() => defaultVisualisation;
}