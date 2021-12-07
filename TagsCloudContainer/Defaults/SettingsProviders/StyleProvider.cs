using Mono.Options;
using System.Drawing;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class StyleProvider : ICliSettingsProvider
{
    public string FontFamilyName { get; set; } = "Comic Sans MS";

    public double MaxSize { get; set; } = 50;

    public Color BrushColor { get; set; } = Color.White;

    public Style GetStyle(ITag tag)
    {
        var font = new Font(new FontFamily(FontFamilyName), (float)(tag.RelativeSize * MaxSize));
        return new(font, new SolidBrush(BrushColor));
    }

    public OptionSet GetCliOptions()
    {
        var options = new OptionSet()
        {
            {"font-family=", $"Sets the font family name for tags. Defaults to {FontFamilyName}", v => FontFamilyName = v },
            {"max-size=", $"Sets the max size for tags. Defaults to {MaxSize}", (double v) => MaxSize = v },
            {"color=", $"Sets the color for tags. Defaults to {BrushColor.Name}", (Color v) => BrushColor = v },
        };

        return options;
    }
}
