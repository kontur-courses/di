using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualizationTests.Utils;

public class RectanglesCloudVisualizator
{
    private readonly ITagsCloudLayouter layouter;
    private readonly Bitmap image;
    private readonly int radius;
    
    public RectanglesCloudVisualizator(ITagsCloudLayouter layouter)
    {
        if (!layouter.Rectangles.Any())
            throw new ArgumentException("Layouter rectangles should contain elements");
        this.layouter = layouter;
        radius = (int)layouter.Rectangles.GetDistanceToMostDistantPoint(layouter.Center);
        image = new Bitmap(radius * 2, radius * 2);
    }

    public Bitmap Draw()
    {
        using (var graphics = Graphics.FromImage(image))
        {
            Centering(graphics);
            FillBackground(graphics);
            DrawRectangles(graphics);
            DrawShape(graphics);
            graphics.Save();
        }

        return image;
    }

    private void Centering(Graphics graphics)
    {
        graphics.TranslateTransform(radius - layouter.Center.X, radius - layouter.Center.Y);
    }
    
    private void FillBackground(Graphics graphics)
    {
        graphics.Clear(Color.White);
    }

    private void DrawRectangles(Graphics graphics)
    {
        graphics.FillRectangles(Brushes.Aqua, layouter.Rectangles.ToArray());
        graphics.DrawRectangles(Pens.Black, layouter.Rectangles.ToArray());
    }

    private void DrawShape(Graphics graphics)
    {
        var offset = new Size(-radius, -radius);
        var centerWithOffset = layouter.Center.WithOffset(offset);
        var inscribedRectangle = new Rectangle(centerWithOffset, new Size(radius * 2, radius * 2));
        graphics.DrawEllipse(Pens.Brown, inscribedRectangle);
    }
}