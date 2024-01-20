using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Color = SixLabors.ImageSharp.Color;
using PointF = SixLabors.ImageSharp.PointF;
using Rectangle = System.Drawing.Rectangle;

namespace TagsCloudContainer;

public class ImageGenerator : IDisposable
{
    private readonly Image<Rgba32> image;
    private readonly string outputPath;
    private readonly int fontSize;
    private readonly FontFamily family;
    private readonly ImageEncoder encoder;
    private readonly Func<int, (byte r, byte g, byte b, byte a)> scheme;

    public ImageGenerator(string outputPath, (ImageEncoder encoding, string ext) encoder,
        string fontPath, int fontSize, int width, int height)
        : this(outputPath, encoder, fontPath, fontSize, width, height, Color.FromRgb(7, 42, 22),
            frequency => (211, 226, 157, (byte)Math.Min(255, 100 + frequency * 10)))
    {
    }

    public ImageGenerator(string outputPath, (ImageEncoder encoding, string ext) encoder,
        string fontPath, int fontSize, int width, int height, Color bg)
        : this(outputPath, encoder, fontPath, fontSize, width, height, bg,
            frequency => (211, 226, 157, (byte)Math.Min(255, 100 + frequency * 10)))
    {
    }

    public ImageGenerator(string outputPath, (ImageEncoder encoding, string ext) encoder,
        string fontPath, int fontSize, int width, int height, Func<int, (byte r, byte g, byte b, byte a)> scheme)
        : this(outputPath, encoder, fontPath, fontSize, width, height,
            Color.FromRgb(7, 42, 22), scheme)
    {
    }

    public ImageGenerator(string outputPath, (ImageEncoder encoding, string ext) encoder,
        string fontPath, int fontSize, int width, int height,
        Color bg, Func<int, (byte r, byte g, byte b, byte a)> scheme)
    {
        image = new Image<Rgba32>(width, height);

        this.outputPath = outputPath + "." + encoder.ext;

        this.encoder = encoder.encoding;

        this.fontSize = fontSize;

        this.scheme = scheme;

        family = new FontCollection().Add(fontPath);

        SetBackground(bg);
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
        var color = scheme(frequency);
        image.Mutate(x => x.DrawText(
            word, FontCreator(fontSize + frequency),
            Color.FromRgba(color.r, color.g, color.b, color.a),
            new PointF(rectangle.X, rectangle.Y))
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