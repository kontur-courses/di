using System.Drawing;

namespace TagCloud.App.CloudCreatorDriver.DrawingSettings;

public class DrawingSettings : IDrawingSettings
{
    public Color BgColor { get; set; }
    public Size PictureSize { get; set; }
    private readonly IWordVisualisation defaultVisualisation;
    
    private readonly List<IWordVisualisation> wordVisualisations;

    public DrawingSettings(IWordVisualisation defaultVisualisation)
    {
        this.defaultVisualisation = defaultVisualisation;
        wordVisualisations = new List<IWordVisualisation>(){defaultVisualisation};
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