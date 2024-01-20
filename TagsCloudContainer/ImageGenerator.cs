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

    public ImageGenerator(string outputPath, string fontPath, int fontSize, int width, int height, ImageEncoder encoder)
    {
        image = new Image<Rgba32>(width, height);

        this.outputPath = outputPath;
        
        this.encoder = encoder;

        this.fontSize = fontSize;

        family = new FontCollection().Add(fontPath);

        SetBackground(Color.FromRgb(7, 42, 22));
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
            word, FontCreator(fontSize + frequency),
            Color.FromRgba(211, 226, 157, (byte)Math.Min(255, 100 + frequency * 10)),
            new PointF(rectangle.X, rectangle.Y))
        );
    }
    
    public void DrawLayout(IEnumerable<Rectangle> rectangles)
    {
        foreach (var tmpRect in rectangles)
        {
            var rectangle = new RectangleF(tmpRect.X, tmpRect.Y, tmpRect.Width, tmpRect.Height);
            image.Mutate(x => x.Draw(Color.FromRgb(211, 226, 157), 2f, rectangle));
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