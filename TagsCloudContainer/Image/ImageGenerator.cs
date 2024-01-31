using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TagsCloudContainer.UI;
using Color = SixLabors.ImageSharp.Color;
using PointF = SixLabors.ImageSharp.PointF;
using Rectangle = System.Drawing.Rectangle;

namespace TagsCloudContainer.Image;

public class ImageGenerator : IDisposable
{
    private readonly Image<Rgba32> image;
    private readonly string outputPath;
    private readonly int fontSize;
    private readonly FontFamily family;
    private readonly ImageEncoder encoder;
    private readonly Color scheme;

    public ImageGenerator(ApplicationArguments args)
    {
        var format = args.Format switch
        {
            "bmp" => ImageEncodings.Bmp,
            "gif" => ImageEncodings.Gif,
            "jpg" => ImageEncodings.Jpg,
            "png" => ImageEncodings.Png,
            "tiff" => ImageEncodings.Tiff,
            _ => ImageEncodings.Jpg
        };

        image = new Image<Rgba32>(args.Resolution[0], args.Resolution[1]);
        outputPath = args.Output + "." + format.ext;
        encoder = format.encoding;
        scheme = Color.FromRgba(
            (byte)args.Scheme[0],
            (byte)args.Scheme[1],
            (byte)args.Scheme[2],
            (byte)args.Scheme[3]
        );
        fontSize = args.FontSize;
        family = new FontCollection().Add(args.FontPath);

        SetBackground(Color.FromRgb(
            (byte)args.Background[0],
            (byte)args.Background[1],
            (byte)args.Background[2])
        );
    }

    private Font FontCreator(int size)
    {
        return family.CreateFont(size, FontStyle.Italic);
    }

    private void SetBackground(Color color)
    {
        image.Mutate(x => x.Fill(color));
    }

    private void DrawWord(string word, int frequency, Rectangle rectangle)
    {
        image.Mutate(x => x.DrawText(
            word, FontCreator(fontSize + frequency), scheme, new PointF(rectangle.X, rectangle.Y))
        );
    }

    public void DrawLayout(IEnumerable<Rectangle> rectangles)
    {
        foreach (var tmpRect in rectangles)
        {
            var rectangle = new RectangleF(tmpRect.X, tmpRect.Y, tmpRect.Width, tmpRect.Height);
            image.Mutate(x => x.Draw(Color.Red, 2f, rectangle));
        }

        image.Save(outputPath, encoder);
    }

    public void DrawTagCloud(List<(string word, int frequency, Rectangle outline)> wordsFrequenciesOutline)
    {
        foreach (var wordFrequencyOutline in wordsFrequenciesOutline)
            DrawWord(wordFrequencyOutline.word, wordFrequencyOutline.frequency, wordFrequencyOutline.outline);
        image.Save(outputPath, encoder);
    }

    public System.Drawing.Size GetOuterRectangle(string word, int frequency)
    {
        var textOption = new TextOptions(FontCreator(fontSize + frequency));
        var size = TextMeasurer.MeasureSize(word, textOption);

        return new System.Drawing.Size((int)size.Width + fontSize / 3, (int)size.Height + fontSize / 3);
    }

    public void Dispose()
    {
        image.Dispose();
    }
}