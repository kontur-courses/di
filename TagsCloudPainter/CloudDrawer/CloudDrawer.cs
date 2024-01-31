﻿using System.Drawing;
using System.Drawing.Drawing2D;
using TagsCloudPainter.Settings;

namespace TagsCloudPainter.Drawer;

public class CloudDrawer
{
    private readonly CloudSettings cloudSettings;
    private readonly TagSettings tagSettings;

    public CloudDrawer(TagSettings tagSettings, CloudSettings cloudSettings)
    {
        this.tagSettings = tagSettings ?? throw new ArgumentNullException(nameof(tagSettings));
        this.cloudSettings = cloudSettings ?? throw new ArgumentNullException(nameof(cloudSettings));
    }

    public Bitmap DrawCloud(TagsCloud cloud, int imageWidth, int imageHeight)
    {
        if (cloud.Tags.Count == 0)
            throw new ArgumentException("rectangles are empty");
        if (imageWidth <= 0 || imageHeight <= 0)
            throw new ArgumentException("either width or height of rectangle size is not positive");

        var drawingScale = CalculateObjectDrawingScale(cloud.GetWidth(), cloud.GetHeight(), imageWidth, imageHeight);
        var bitmap = new Bitmap(imageWidth, imageHeight);
        using var graphics = Graphics.FromImage(bitmap);
        using var pen = new Pen(tagSettings.TagColor);
        {
            graphics.TranslateTransform(-cloud.Center.X, -cloud.Center.Y);
            graphics.ScaleTransform(drawingScale, drawingScale, MatrixOrder.Append);
            graphics.TranslateTransform(cloud.Center.X, cloud.Center.Y, MatrixOrder.Append);
            graphics.Clear(cloudSettings.BackgroundColor);
            foreach (var tag in cloud.Tags)
            {
                var font = new Font(tagSettings.TagFontName, tag.Key.FontSize);
                graphics.DrawString(tag.Key.Value, font, pen.Brush, tag.Value.Location);
            }
        }
        ;
        return bitmap;
    }

    public static float CalculateObjectDrawingScale(float width, float height, float imageWidth, float imageHeight)
    {
        var scale = 1f;
        var scaleAccuracy = 0.05f;
        var widthScale = scale;
        var heightScale = scale;
        if (width * scale > imageWidth)
            widthScale = imageWidth / width - scaleAccuracy;
        if (height * scale > imageHeight)
            heightScale = imageHeight / height - scaleAccuracy;
        return Math.Min(widthScale, heightScale);
    }
}