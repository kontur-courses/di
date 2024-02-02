using System.Drawing;


// Changes opacity of color based on font size
public class OpacityColorProvider : IColorProvider
{
    private readonly RenderOptions options;

    public OpacityColorProvider(RenderOptions options)
    {
        this.options = options;
    }

    public Color GetColor(WordLayout layout)
    {
        var baseC = options.ColorScheme.FontColor;

        var opacity = 255 * 0.4;
        opacity += 255 * 0.5 * TagCloudHelpers.GetMultiplier(
            layout.FontSize, options.MinFontSize, options.MaxFontSize);

        var newColor = Color.FromArgb((int)opacity, baseC.R, baseC.G, baseC.B);

        return newColor;
    }
}

