using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace TagsCloudContainer.Render.CircularCloud;

public class CircularCloudRender : BaseCloudRender<CircularCloudRenderOptions>
{
    private readonly CircularCloudLayouter _layouter;

    public CircularCloudRender(CircularCloudRenderOptions options, CircularCloudLayouter layouter) : base(options)
    {
        _layouter = layouter;
    }

    public override Image Render((string Word, int Count)[] words)
    {
        var image = new Image<Rgba32>(Options.ImageWidth, Options.ImageHeight);
        image.Mutate(context => context.Fill(Options.BackgroundColor));

        foreach (var wordWithCount in words)
        {
            var scaledFontSize = ScaleFontSize(Options.MinimumFontSize, wordWithCount.Count);
            var font = SystemFonts.CreateFont(Options.FontName, scaledFontSize, FontStyle.Regular);
            var wordSize = TextMeasurer.Measure(wordWithCount.Word, new RendererOptions(font));
            var rectangle = _layouter.PutNextRectangle(new Size((int) wordSize.Width, (int) wordSize.Height));
            image.Mutate(x =>
                x.DrawText(wordWithCount.Word, font, Options.NextTextColor, new PointF(rectangle.X, rectangle.Y)));
        }

        return image;
    }

    private int ScaleFontSize(int fontSize, int wordQuantity)
    {
        const double magicLogarithmBase = 1.02;
        return (int) Math.Ceiling(fontSize + Math.Log(wordQuantity, magicLogarithmBase));
    }
}