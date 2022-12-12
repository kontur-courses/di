using System.Diagnostics;

namespace TagCloudContainer;

public static class Sizes
{
    private static readonly Dictionary<string, Size> _sizes = new Dictionary<string, Size>()
    {
        { "1920x1080", new Size(1920, 1080) },
        { "1600x900", new Size(1600, 900) },
        { "1400x1050", new Size(1400, 1050) },
        { "1280x768", new Size(1280, 768) },
    };

    public static Size Get(string screenResolution)
    {
        if (string.IsNullOrEmpty(screenResolution))
            return _sizes["1920x1080"];
        if (!_sizes.ContainsKey(screenResolution))
            throw new ArgumentException("Invalid screen resolution");
        return _sizes[screenResolution];
    }

    public static Dictionary<string, Size> GetAll() => _sizes;
}