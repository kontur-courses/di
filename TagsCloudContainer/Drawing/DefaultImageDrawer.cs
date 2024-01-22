﻿using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.Common;
using TagsCloudContainer.Drawing.Colorers;
using TagsCloudContainer.DrawingOptions;
using TagsCloudContainer.TagCloudForming;

namespace TagsCloudContainer.Drawing;

public class DefaultImageDrawer : IImageDrawer
{
    private readonly IReadOnlyDictionary<string, Word> _distributedWords;
    private readonly Options _options;
    private readonly IWordColorer _colorer;
    public DefaultImageDrawer(IWordCloudDistributorProvider cloudDistributorProvider, IOptionsProvider optionsProvider, IWordColorer colorer)
    {
        _distributedWords = cloudDistributorProvider.DistributedWords;
        _options = optionsProvider.Options;
        _colorer = colorer;
    }

    public Bitmap DrawImage()
    {
        var bitmap = new Bitmap(_options.ImageSize.Width, _options.ImageSize.Height);
        var offset = new Point(_options.ImageSize.Width / 2, _options.ImageSize.Height / 2);
        var graphics = Graphics.FromImage(bitmap);
        graphics.FillRectangle(new SolidBrush(_options.BackgroundColor), 0, 0, bitmap.Width, bitmap.Height);

        foreach (var (value, word) in _distributedWords)
        {
            var sizeAdd = _options.FrequencyScaling * (word.Frequency - 1);
            var newFont = new Font(_options.Font.FontFamily, _options.Font.Size + sizeAdd, _options.Font.Style);
            graphics.DrawString(value, newFont, new SolidBrush(_colorer.GetWordColor(value, word.Frequency)),
                word.Rectangle with {X = word.Rectangle.X + offset.X, Y = word.Rectangle.Y + offset.Y});
        }

        return bitmap;
    }

    public static void SaveImage(Bitmap bitmap, string dirPath, string filename, ImageFormat imageFormat)
    {
        if (string.IsNullOrWhiteSpace(filename) || filename.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            throw new ArgumentException("The provided filename is not valid.");

        try
        {
            Directory.CreateDirectory(dirPath);
        }
        catch (Exception e)
        {
            throw new ArgumentException("The provided directory path is not valid.", e);
        }

        bitmap.Save(Path.Combine(dirPath, filename), imageFormat);
    }
}