using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace TagsCloudVisualization;

public static class LayoutVisualizer
{
    public static void CreateVisualization(
        IList<RectangleF> rectangles,
        Size imageSize,
        Color backgroundColor,
        float thickness,
        Color borderColor,
        string fileName)
    {
        using var image = new Image<Rgba32>(imageSize.Width, imageSize.Height);

        image.Mutate(ctx =>
        {
            ctx.Clear(backgroundColor);

            foreach (var figure in rectangles)
                ctx.Draw(borderColor, thickness, figure);
        });

        image.SaveAsPng(fileName);
    }
}