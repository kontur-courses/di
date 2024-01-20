using System.Drawing;
using TagsCloudContainer.DrawingOptions;

namespace TagsCloudContainer.Drawing.Colorers;

public class DefaultWordColorer : IWordColorer
{
    private readonly Color _color;
    
    public DefaultWordColorer(IOptionsProvider optionsProvider)
    {
        _color = optionsProvider.Options.FontColor;
    }

    public Color GetWordColor(string word, int wordFrequency)
        => _color;
}