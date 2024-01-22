using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.WordsAnalyzers;

namespace TagsCloudVisualization;

public class TagsCloudVisualizator
{
    private readonly ITagsCloudLayouter layouter;
    private readonly Bitmap image;
    private readonly TagProvider tagProvider;
    
    public TagsCloudVisualizator(ITagsCloudLayouter layouter, TagProvider tagProvider)
    {
        this.tagProvider = tagProvider;
        this.layouter = layouter;
        image = new Bitmap(2000, 2000);
    }

    public TagsCloudVisualizator(ITagsCloudLayouter layouter)
    {
        if (!layouter.Rectangles.Any())
            throw new ArgumentException("Layouter rectangles should contain elements");
        this.layouter = layouter;
        var radius = (int)layouter.Rectangles.GetDistanceToMostDistantPoint(layouter.Center);
        image = new Bitmap(radius * 2, radius * 2);
    }

    public Bitmap DrawTagsCloud()
    {
        using (var graphics = Graphics.FromImage(image))
        {
            FillBackground(graphics);
            DrawTags(graphics);
            Centering(graphics);
            graphics.Save();
        }

        return image;
    }

    public Bitmap DrawRectanglesCloud()
    {
        using (var graphics = Graphics.FromImage(image))
        {
            FillBackground(graphics);
            DrawRectangles(graphics);
            Centering(graphics);
            graphics.Save();
        }

        return image;
    }

    private void Centering(Graphics graphics)
    {
        var radius = (float)layouter.Rectangles.GetDistanceToMostDistantPoint(layouter.Center);
        graphics.TranslateTransform(radius - layouter.Center.X, radius - layouter.Center.Y);
    }
    
    private void FillBackground(Graphics graphics)
    {
        graphics.Clear(Color.Black);
    }

    private void DrawTags(Graphics graphics)
    {
        var fontsize = 200;
        foreach (var tag in tagProvider.GetTags())
        {
            var font = new Font("Times New Roman", fontsize * (float)tag.Coeff);
            var rectangle = layouter.PutNextRectangle(graphics.MeasureString(tag.Word, font).ToSize());
            graphics.DrawRectangle(Pens.Azure, rectangle);
            graphics.DrawString(tag.Word, font, new SolidBrush(Color.Aquamarine), rectangle.Location);
        }
    }

    private void DrawRectangles(Graphics graphics)
    {
        graphics.FillRectangles(Brushes.Aqua, layouter.Rectangles.ToArray());
        graphics.DrawRectangles(Pens.Black, layouter.Rectangles.ToArray());
    }
}