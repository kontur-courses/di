using System.Drawing.Imaging;
using TagCloud.Curves;
using TagCloud.Files;

namespace TagCloud;

public static class Helper
{
    public static ImageFormat GetImageFormat(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        return extension.ToLower() switch
        {
            ".png" => ImageFormat.Png,
            ".bmp" => ImageFormat.Bmp,
            ".ico" => ImageFormat.Icon,
            ".jpg"  => ImageFormat.Jpeg,
            ".jpeg" => ImageFormat.Jpeg,
            _ => throw new ArgumentException($"This extension {extension} are not supported!")
        };
    }

    public static ICurve GetCurveByName(string curveName)
    {
        return curveName.ToLower() switch
        {
            ArchimedeanSpiral.Name => new ArchimedeanSpiral(),
            _ => throw new ArgumentException($"There is no {curveName} algorithm")
        };
    }
}