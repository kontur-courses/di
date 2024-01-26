using System.Drawing;
using TagsCloudCore.BuildingOptions;

namespace TagsCloudCore.Drawing.Colorers;

public class DefaultWordColorer : IWordColorer
{
    private readonly Color _color;
    
    public DefaultWordColorer(IDrawingOptionsProvider drawingOptionsProvider)
    {
        _color = drawingOptionsProvider.DrawingOptions.FontColor;
    }

    public Color GetWordColor(string word, int wordFrequency)
        => _color;
}