using System.Drawing;
using CircularCloudLayouter.Domain;
using TagCloudCreator.Domain.Settings;
using TagCloudCreator.Infrastructure;
using TagCloudCreator.Interfaces;

namespace TagCloudCreator.Domain;

public class WordSizeCalculator : IWordSizeCalculator
{
    private readonly Graphics _graphics;
    private readonly TagCloudPaintSettings _settings;

    public WordSizeCalculator(Graphics graphics, TagCloudPaintSettings settings)
    {
        _graphics = graphics;
        _settings = settings;
    }

    public ImmutableSize GetSizeFor(string word, int fontSize)
    {
        var size = Size.Ceiling(_graphics.MeasureString(word, _settings.BasicFont.WithSize(fontSize)));
        return new ImmutableSize(size.Width, size.Height);
    }
}