using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace TagsCloudContainerCore.Helpers;

[SuppressMessage("Interoperability", "CA1416", MessageId = "Проверка совместимости платформы")]
public static class WinSaver
{
    private static readonly Regex NameRegex = new(@"(?<=[\\\/])[^\\\/]+?\.(.+)$", RegexOptions.Compiled);

    public static void Save(Bitmap picture, string outPath)
    {
        if (!NameRegex.IsMatch(outPath))
        {
            var name = DateTime.Now.ToString("dd-MMMM-yyyy-hh-mm") + ".png";

            picture.Save((outPath + "\\" + name).Replace("\"", ""), ImageFormat.Png);
            
            return;
        }

        var f = NameRegex.Match(outPath).Groups[1].Value;
        var format = GetFormat(f);

        picture.Save(outPath, format);
    }

    private static ImageFormat GetFormat(string form)
    {
        switch (form)
        {
            case "png":
                return ImageFormat.Png;
            case "jpg":
            case "jpeg":
                return ImageFormat.Jpeg;
            case "bmb":
                return ImageFormat.Bmp;
            default:
                throw new FormatException($"Неподдерживаемый формат изображения: \"{form}\"");
        }
    }
}