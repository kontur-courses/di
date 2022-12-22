namespace TagCloudContainer.Additions.Models;

public static class Colors
{
    private static ColorConverter _colorConverter = new ColorConverter();
    
    private static readonly Dictionary<string, Color> _colors = new Dictionary<string, Color>()
    {
        { "Yellow", (Color)_colorConverter.ConvertFromString("#e9c46a") },
        { "Green",  (Color)_colorConverter.ConvertFromString("#2a9d8f") },
        { "Red",    (Color)_colorConverter.ConvertFromString("#ae2012") },
        { "Cyan",   (Color)_colorConverter.ConvertFromString("#a2d2ff") },
        { "White",  (Color)_colorConverter.ConvertFromString("#edede9") },
        { "Black",  (Color)_colorConverter.ConvertFromString("#001524") },
        { "Purple", (Color)_colorConverter.ConvertFromString("#f15bb5") },
    };

    public static Color Get(string colorName)
    {
        if (string.IsNullOrEmpty(colorName))
            return _colors["Black"];
        if (!_colors.ContainsKey(colorName))
            throw new ArgumentException("Invalid color Name");
        return _colors[colorName];
    }

    public static Dictionary<string, Color> GetAll() => _colors;
}