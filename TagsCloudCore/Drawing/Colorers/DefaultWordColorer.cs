using System.Drawing;
using TagsCloudCore.BuildingOptions;
using TagsCloudCore.Common;

namespace TagsCloudCore.Drawing.Colorers;

public class DefaultWordColorer : IWordColorer
{
    private readonly Lazy<IDrawingOptionsProvider> _drawingOptionsProvider;
    
    public DefaultWordColorer(Lazy<IDrawingOptionsProvider> drawingOptionsProvider)
    {
        _drawingOptionsProvider = drawingOptionsProvider;
    }

    public Color GetWordColor(string word, int wordFrequency)
        => _drawingOptionsProvider.Value.DrawingOptions.FontColor;

    public string Name => AppSettings.DefaultColorerName;
}