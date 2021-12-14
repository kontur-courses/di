using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainerCore.Helpers;

[SuppressMessage("Interoperability", "CA1416", MessageId = "Проверка совместимости платформы")]
public static class DrawHelper
{
    public static void Paint(
        string outPath,
        IEnumerable<TagToRender> tags,
        Size imageSize,
        int backgroundColorHex)
    {
        using var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);
        var bitmapCenter = new Point(imageSize.Width / 2, imageSize.Height / 2);

        using (var brush = new SolidBrush(Color.FromArgb(backgroundColorHex)))
        {
            graphics.FillRectangle(brush, new Rectangle(0, 0, imageSize.Width, imageSize.Height));
        }

        graphics.TranslateTransform(bitmapCenter.X, bitmapCenter.Y);

        foreach (var tag in tags)
        {
            if (tag.FontSize <= 0) continue;
            var color = Color.FromArgb(tag.ColorHex);
            using var font = new Font(tag.FontName, tag.FontSize);
            using var fontBrush = new SolidBrush(color);
            graphics.DrawString(tag.Value, font, fontBrush, tag.Location);
        }

        bitmap.Save(outPath, ImageFormat.Png);
    }
}