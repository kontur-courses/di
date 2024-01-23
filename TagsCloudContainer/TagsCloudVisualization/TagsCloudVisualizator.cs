using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Common;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.WordsAnalyzers;

namespace TagsCloudVisualization;

public class TagsCloudVisualizator
{
    private readonly ITagsCloudLayouter layouter;
    private readonly TagProvider tagProvider;
    private readonly IImageHolder imageHolder;
    private readonly Palette palette;
    
    public TagsCloudVisualizator(ITagsCloudLayouter layouter, IImageHolder imageHolder, TagProvider tagProvider, Palette palette)
    {
        this.tagProvider = tagProvider;
        this.layouter = layouter;
        this.palette = palette;
        this.imageHolder = imageHolder;
    }

    public void DrawTagsCloud()
    {
        using (var graphics = imageHolder.StartDrawing())
        {
            FillBackground(graphics);
            DrawTags(graphics);
            graphics.Save();
        }

        imageHolder.UpdateUi();
    }

    private void FillBackground(Graphics graphics)
    {
        graphics.Clear(palette.BackgroundColor);
    }

    private void DrawTags(Graphics graphics)
    {
        using (layouter)
        {
            var fontsize = 42;
            foreach (var tag in tagProvider.GetTags())
            {
                var font = new Font("Times New Roman", fontsize * (float) tag.Coeff);
                var rectangle = layouter.PutNextRectangle(graphics.MeasureString(tag.Word, font).ToSize());
                var color = tag.Coeff > 0.75 ? palette.PrimaryColor :
                    tag.Coeff > 0.35 ? palette.SecondaryColor : palette.TertiaryColor;
                graphics.DrawString(tag.Word, font, new SolidBrush(color), rectangle.Location);
            }
        }
    }
}