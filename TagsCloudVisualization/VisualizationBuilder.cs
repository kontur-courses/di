using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TagsCloud.Entities;

namespace TagsCloudVisualization;

public class VisualizationBuilder
{
    private readonly Size canvasSize;
    private readonly Color backgroundColor;
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
            tags.ForEach(tag => ctx.DrawText(tag.InnerText!, tag.TextFont!, tag.TextColor, tag.Location));
        });

        return this;
    }

    public void SaveAs(string filename, IImageEncoder? encoder = null)
    {
        encoder ??= new PngEncoder();
        image.Save(filename, encoder);
    }
}