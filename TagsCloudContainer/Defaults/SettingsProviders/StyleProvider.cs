using Mono.Options;
using System.Drawing;
using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class StyleProvider : ICliSettingsProvider
{
    public string FontFamilyName { get; private set; } = "Comic Sans MS";
    public double MinSize { get; private set; } = 50;
    public Color BrushColor { get; private set; } = Color.White;
    public Style GetStyle(ITag tag)
    {
        var font = new Font(new FontFamily(FontFamilyName), (float)((1 + tag.RelativeSize) * MinSize));
        return new(font, new SolidBrush(BrushColor));
    }

    public OptionSet GetCliOptions()
    {
        var options = new OptionSet()
        {
            {"font-family=", $"Sets the font family name for tags. Defaults to {FontFamilyName}", v => FontFamilyName = v },
            {"min-size=", $"Sets the min size for tags. Defaults to {MinSize}", (double v) => MinSize = v },
            {"color=", $"Sets the color for tags. Defaults to {BrushColor.Name}", (Color v) => BrushColor = v },
        };

        return options;
    }
}
