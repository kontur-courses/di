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

    public VisualizationBuilder CreateImageFrom(List<CloudTag> tags)
    {
        image = new Image<Rgba32>(canvasSize.Width, canvasSize.Height);
        image.Mutate(ctx =>
        {
            ctx.Clear(backgroundColor);
            tags.ForEach(tag =>
            {
                var location = tag.BoundRectangle.Location;

                if (tag.IsRotated)
                {
                    var offsetLocation = new PointF(location.X + tag.BoundRectangle.Width, location.Y);
                    var options = new DrawingOptions
                    {
                        Transform = Matrix3x2Extensions.CreateRotationDegrees(90, offsetLocation)
                    };

                    ctx.DrawText(options, tag.InnerText, tag.TextFont, tag.TextColor, offsetLocation);
                    return;
                }

                ctx.DrawText(tag.InnerText, tag.TextFont, tag.TextColor, location);
            });

            // Only for testing (debug state):
            tags.ForEach(tag => ctx.Draw(Color.Black, 1f, tag.BoundRectangle));
        });

        return this;
    }

    public void SaveAs(string filename, IImageEncoder? encoder = null)
    {
        encoder ??= new PngEncoder();
        image.Save(filename, encoder);
    }
}