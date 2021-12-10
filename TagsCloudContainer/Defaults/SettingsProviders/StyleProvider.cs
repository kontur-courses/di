using Mono.Options;
using System.Drawing;
using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class StyleProvider : ICliSettingsProvider
{
    private static string fontFamilyName = "Comic Sans MS";
    private static double maxSize = 50;
    private static Color brushColor = Color.White;

    public string FontFamilyName { get => fontFamilyName; private set => fontFamilyName = value; }
    public double MinSize { get => maxSize; private set => maxSize = value; }
    public Color BrushColor { get => brushColor; private set => brushColor = value; }
    public Style GetStyle(ITag tag)
    {
        var font = new Font(new FontFamily(FontFamilyName), (float)((1+tag.RelativeSize) * MinSize));
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
