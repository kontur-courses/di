using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class DrawingCircularCloudLayoutTracer : ICircularCloudLayoutTracer, IDisposable
{
    private readonly Bitmap bitmap;
    private readonly Point center;
    private readonly Pen circlePen;
    private readonly Graphics graphics;
    private readonly Rectangle imageRectangle;
    private readonly Pen linePen;
    private readonly Brush numbersColor;
    private readonly Font numbersFont;
    private readonly Pen rectanglePen;
    private readonly List<Rectangle> rectangles = new();
    private readonly Pen shiftingPen;
    private int counter;
    private Point previousRectangleCenter;
    private double rectangleAngleRadius;
    private double spiralRadius;

    public DrawingCircularCloudLayoutTracer(
        int width,
        int height,
        Point center,
        Pen linePen,
        Pen circlePen,
        Font numbersFont,
        Brush numbersColor,
        Pen rectanglePen,
        Brush backgroundColor,
        Pen shiftingPen)
    {
        bitmap =
            bitmap = new(width, height);
        graphics = Graphics.FromImage(bitmap);
        imageRectangle = new(0, 0, width, height);
        graphics.FillRectangle(backgroundColor, imageRectangle);
        this.center = center;
        this.linePen = linePen;
        this.circlePen = circlePen;
        this.numbersFont = numbersFont;
        this.numbersColor = numbersColor;
        this.rectanglePen = rectanglePen;
        this.shiftingPen = shiftingPen;
    }

    public DrawingCircularCloudLayoutTracer(int width, int height, Point center) : this(
        width,
        height,
        center,
        new(Color.Blue, 1),
        new(Color.Yellow, 0.5f),
        new(FontFamily.GenericMonospace, 10, FontStyle.Regular, GraphicsUnit.Pixel),
        Brushes.Black,
        new(Color.Red, 1),
        Brushes.White,
        new(Color.Gray, 0.7f))
    {
    }

    public void TraceRectangle(Rectangle rectangle)
    {
        if (imageRectangle.Contains(rectangle))
        {
            rectangles.Add(rectangle);
            rectangleAngleRadius = Math.Max(rectangleAngleRadius,
                rectangle.GetFarthestDeltaFromTarget(center)
                    .DistanceTo(Point.Empty));
        }
    }

    public void TraceCirclePoint(Point point)
    {
        if (imageRectangle.Contains(point))
            spiralRadius = Math.Max(spiralRadius, point.DistanceTo(center));
    }

    public void TraceShifting(Point from, Point to)
    {
        if (imageRectangle.Contains(from) && imageRectangle.Contains(to))
            graphics.DrawLine(shiftingPen, from, to);
    }

    public void Dispose()
    {
        numbersFont.Dispose();
        rectanglePen.Dispose();
        linePen.Dispose();
        graphics.Dispose();
        bitmap.Dispose();
        GC.SuppressFinalize(this);
    }

    public void SaveToFile(string fileName)
    {
        DrawRectangles();

        DrawCircles();

        graphics.Save();
        using var ms = new MemoryStream();
        bitmap.Save(ms, ImageFormat.Jpeg);
        File.WriteAllBytes(fileName, ms.ToArray());
    }

    private void DrawCircles()
    {
        graphics.DrawEllipse(circlePen,
            (int)(center.X - spiralRadius), (int)(center.Y - spiralRadius), (int)(spiralRadius * 2),
            (int)(spiralRadius * 2));
        graphics.DrawEllipse(circlePen,
            (int)(center.X - rectangleAngleRadius), (int)(center.Y - rectangleAngleRadius),
            (int)(rectangleAngleRadius * 2), (int)(rectangleAngleRadius * 2));
    }

    private void DrawRectangles()
    {
        foreach (var rectangle in rectangles)
        {
            var rectangleCenter = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
            if (!previousRectangleCenter.IsEmpty) graphics.DrawLine(linePen, previousRectangleCenter, rectangleCenter);
            previousRectangleCenter = rectangleCenter;

            graphics.DrawString((counter++).ToString(), numbersFont, numbersColor, rectangle);
            graphics.DrawRectangle(rectanglePen, rectangle);
        }
    }
}