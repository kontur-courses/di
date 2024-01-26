using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.BuildingOptions;
using TagsCloudContainer.Common;
using TagsCloudContainer.Drawing.Colorers;
using TagsCloudContainer.TagCloudForming;

namespace TagsCloudContainer.Drawing;

public class DefaultImageDrawer : IImageDrawer
{
    private readonly IReadOnlyDictionary<string, WordData> _distributedWords;
    private readonly DrawingOptions _drawingOptions;
    private readonly IWordColorer? _colorer;

    public DefaultImageDrawer(IWordCloudDistributorProvider cloudDistributorProvider,
        IDrawingOptionsProvider drawingOptionsProvider, ICommonOptionsProvider commonOptionsProvider)
    {
        _distributedWords = cloudDistributorProvider.DistributedWords;
        _drawingOptions = drawingOptionsProvider.DrawingOptions;
        _colorer = commonOptionsProvider.CommonOptions.WordColorer;
    }

    public Bitmap DrawImage()
    {
        var bitmap = new Bitmap(_drawingOptions.ImageSize.Width, _drawingOptions.ImageSize.Height);
        var offset = new Point(_drawingOptions.ImageSize.Width / 2, _drawingOptions.ImageSize.Height / 2);
        var graphics = Graphics.FromImage(bitmap);
        graphics.FillRectangle(new SolidBrush(_drawingOptions.BackgroundColor), 0, 0, bitmap.Width, bitmap.Height);

        foreach (var (value, word) in _distributedWords)
        {
            var sizeAdd = _drawingOptions.FrequencyScaling * (word.Frequency - 1);
            var newFont = new Font(_drawingOptions.Font.FontFamily, _drawingOptions.Font.Size + sizeAdd,
                _drawingOptions.Font.Style);
            var color = _colorer?.GetWordColor(value, word.Frequency) ?? _drawingOptions.FontColor;
            graphics.DrawString(value, newFont, new SolidBrush(color),
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