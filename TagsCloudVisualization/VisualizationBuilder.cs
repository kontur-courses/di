using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace TagsCloudVisualization;

public class VisualizationBuilder
{
    private readonly Color backgroundColor;
    private readonly Size canvasSize;
    private Image<Rgba32> image;

    public VisualizationBuilder(Size canvasSize, Color backgroundColor)
    {
        this.canvasSize = canvasSize;
        this.backgroundColor = backgroundColor;
    }

    public VisualizationBuilder CreateImageFrom(HashSet<WordTagGroup> wordGroups)
    {
        image = new Image<Rgba32>(canvasSize.Width, canvasSize.Height);
        image.Mutate(ctx =>
        {
            ctx.Clear(backgroundColor);

            foreach (var group in wordGroups)
            {
                var location = group.VisualInfo.BoundsRectangle.Location;

                if (group.VisualInfo.IsRotated)
                {
                    var offsetLocation = new PointF(location.X + group.VisualInfo.BoundsRectangle.Width, location.Y);
                    var options = new DrawingOptions
                    {
                        Transform = Matrix3x2Extensions.CreateRotationDegrees(90, offsetLocation)
                    };

                    ctx.DrawText(
                        options,
                        group.WordInfo.Text,
                        group.VisualInfo.TextFont,
                        group.VisualInfo.TextColor,
                        offsetLocation);
                }
                else
                {
                    ctx.DrawText(
                        group.WordInfo.Text,
                        group.VisualInfo.TextFont,
                        group.VisualInfo.TextColor,
                        location);
                }
            }
        });

        return this;
    }

    public void SaveAs(string filename, IImageEncoder encoder = null)
    {
        encoder ??= new PngEncoder();
        image.Save(filename, encoder);
    }
}