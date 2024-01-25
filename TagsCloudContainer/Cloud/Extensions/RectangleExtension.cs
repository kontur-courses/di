using System.Drawing;

namespace TagsCloudContainer;

public static class RectangleExtension
{
    public static Point DecreasingCoordinate(this Point selfPoint, Point otherPoint) =>
        new(selfPoint.X + otherPoint.X, selfPoint.Y + otherPoint.Y);
    
    public static Point IncreasingCoordinate(this Point selfPoint, Point otherPoint) =>
        new(selfPoint.X - otherPoint.X, selfPoint.Y - otherPoint.Y);
    
    public static Point GetCenter(this Rectangle rectangle) =>
        new(rectangle.Location.X + rectangle.Width / 2, rectangle.Location.Y + rectangle.Height / 2);
    
    public static bool IsIntersectOthersRectangles(this Rectangle rectangle, List<Rectangle> rectangles) => 
        rectangles.All(rect => !rectangle.IntersectsWith(rect));
    
}