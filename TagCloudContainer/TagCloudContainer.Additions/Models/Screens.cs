namespace TagCloudContainer.Additions.Models;

public static class Screens
{
    private static readonly string[] _sizes = 
    {
        "1920x1080",
        "1600x900",
        "1400x1050",
        "1024x768",
        "600x600",
    };

    public static string[] Sizes
    {
        get => _sizes;
    }
}