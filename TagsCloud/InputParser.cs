using SixLabors.ImageSharp;

namespace TagsCloud;

public static class InputParser
{
    public static Color ParseBackgroundColor(string hex)
    {
        return Color.TryParseHex(hex, out var color) ? color : Color.White;
    }

    public static Color[] ParseTagColors(HashSet<string> hexes)
    {
        var colors = new HashSet<Color>();

        foreach (var hex in hexes)
            if (Color.TryParseHex(hex, out var color))
                colors.Add(color);

        return colors.Count == 0 ? new[] { Color.Black } : colors.ToArray();
    }
}