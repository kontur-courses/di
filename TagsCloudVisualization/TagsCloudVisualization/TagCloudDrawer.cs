using System.Drawing;

namespace TagsCloudVisualization;

public class TagCloudDrawer
{
    private const string DefaultPath = "..\\..\\..\\..\\..\\..\\Results";

    private Bitmap _bitmap;
    private Graphics _graphics;
    private Pen _pen;
    private CircularCloudLayouter _layouter;

    public TagCloudDrawer(int windowWidth, int windowHeight, int borderWidth, Color borderColor,
        CircularCloudLayouter layouter)
    {
        _layouter = layouter;
        _bitmap = new Bitmap(windowWidth, windowHeight);
        _graphics = Graphics.FromImage(_bitmap);
        _pen = new Pen(borderColor, borderWidth);
    }

    public void DrawRandomRectangles(int count, Size minSize, Size maxSize) =>
        DrawRandomRectangles(count,
            minSize.Width,
            maxSize.Width,
            minSize.Height,
            maxSize.Height);

    public void DrawRandomRectangles(int count, int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        var dWidth = maxWidth - minWidth;
        var dHeight = maxHeight - minHeight;
        var rand = new Random();
        for (int i = 0; i < count; i++)
        {
            _graphics.DrawRectangle(_pen,
                _layouter.PutNextRectangle(
                    new Size(minWidth + rand.Next(dWidth), minHeight + rand.Next(dHeight))));
        }
    }

    public void SaveImage(string path = DefaultPath) =>
        _bitmap.Save($"{path}\\{_layouter.Rectangles.Count}.bmp");
}