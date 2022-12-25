namespace TagCloudContainer.Core.Models;

public static class Screens
{
    private static readonly Size[] _sizes = 
    {
        new Size(1920, 1080),
        new Size(1600, 900),
        new Size(1400, 1050),
        new Size(1024, 768),
        new Size(600, 600),
    };

    public static Size[] Sizes
    {
        get => _sizes;
    }
}