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
    private readonly ITagProvider tagProvider;
    private readonly IImageHolder imageHolder;
    private readonly TagsSettings tagsSettings;
    
    public TagsCloudVisualizator(ITagsCloudLayouter layouter, IImageHolder imageHolder, ITagProvider tagProvider, TagsSettings tagsSettings)
    {
        this.tagProvider = tagProvider;
        this.layouter = layouter;
        this.tagsSettings = tagsSettings;
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
        graphics.Clear(tagsSettings.BackgroundColor);
    }

    private void DrawTags(Graphics graphics)
    {
        using (layouter)
        {
            var fontsize = 42;
            foreach (var tag in tagProvider.GetTags())
            {
                var font = new Font(tagsSettings.Font, fontsize * (float) tag.Coeff);
                var rectangle = layouter.PutNextRectangle(graphics.MeasureString(tag.Word, font).ToSize());
                var color = tag.Coeff > 0.75 ? tagsSettings.PrimaryColor :
                    tag.Coeff > 0.35 ? tagsSettings.SecondaryColor : tagsSettings.TertiaryColor;
                graphics.DrawString(tag.Word, font, new SolidBrush(color), rectangle.Location);
            }
        }
    }
}