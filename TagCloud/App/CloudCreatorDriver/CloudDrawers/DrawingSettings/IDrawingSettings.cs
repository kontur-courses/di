using System.Drawing;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.CloudCreatorDriver.CloudDrawers.DrawingSettings;

public interface IDrawingSettings
{
    Color BgColor { get; set; }
    Size PictureSize { get; set; }
    
    bool HasWordVisualisationSelector();

    IWordsVisualisationSelector GetSelector();

    IDrawingWord GetDrawingWordFromSelector(IWord word, Rectangle rectangle);

    void AddWordVisualisation(IWordVisualisation wordVisualisation);

    IEnumerable<IWordVisualisation> GetWordVisualisations();

    IWordVisualisation GetDefaultVisualisation();
}