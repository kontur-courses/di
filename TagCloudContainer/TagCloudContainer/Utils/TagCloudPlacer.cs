using System.Numerics;
using TagCloudContainer.Additions;
using TagCloudContainer.Additions.Interfaces;

namespace TagCloudContainer;

public class TagCloudPlacer : ITagCloudPlacer
{
    private SortedList<float, Point> _nearestToTheCenterPoints = new SortedList<float, Point>();
    private List<Rectangle> _putRectangles = new List<Rectangle>();
    
    private readonly ITagCloudFormConfig _tagCloudFormConfig;
    private readonly ITagCloudContainerConfig _tagCloudContainerConfig;
    private readonly ITagCloudFormResult _tagCloudFormResult;
    private readonly ISizeInvestigator _sizeInvestigator;

    public TagCloudPlacer(
        ITagCloudFormConfig tagCloudFormConfig, 
        ITagCloudContainerConfig tagCloudContainerConfig,
        ITagCloudFormResult tagCloudFormResult,
        ISizeInvestigator sizeInvestigator)
    {
        _tagCloudContainerConfig = tagCloudContainerConfig;
        _tagCloudFormConfig = tagCloudFormConfig;
        _tagCloudFormResult = tagCloudFormResult;
        _sizeInvestigator = sizeInvestigator;
    }
    
    public Additions.Models.Word PlaceInCloud(Additions.Models.Word word)
    {
        _nearestToTheCenterPoints = _tagCloudContainerConfig.NearestToTheCenterPoints;
        _putRectangles = _tagCloudContainerConfig.PutRectangles;
        
        if (_nearestToTheCenterPoints.Count == 0)
            AddFreePoint(_tagCloudContainerConfig.Center);
        
        word.Size = TextRenderer
            .MeasureText(word.Value, new Font(_tagCloudFormConfig.FontFamily, word.Weight * _tagCloudContainerConfig.StandartSize.Width));

        var nearestFreePoint = GetNearestInsertionPoint(word.Size);
        var rectangle = new Rectangle(nearestFreePoint, word.Size);
        
        _putRectangles.Add(rectangle);
        AddVerticesToFreePoints(rectangle);

        word.Position = new Point(rectangle.X, rectangle.Y);
        word.RotateAngle = rectangle.Size.Width < rectangle.Size.Height ? 90 : 0;
        
        if (_sizeInvestigator.DidFit(word))
            _tagCloudFormResult.FormResult = Result.Fail<ITagCloudFormConfig>("Picture doesn't fit in form area");
        
        return word;
    }
    
    private Point GetNearestInsertionPoint(Size rectangleSize)
    {
        foreach (var nearestPoint in _nearestToTheCenterPoints.Values)
        {
            foreach (var rotateDirection in Enum.GetValues(typeof(Additions.Models.RotateDirections)))
            {
                var insertionPoint = GetInsertionPoint(rectangleSize, nearestPoint, rotateDirection);
                var rectangle = new Rectangle(insertionPoint, rectangleSize);

                if (!DoesItIntersect(rectangle)) return insertionPoint;
            }
        }
        
        throw new Exception("Can't put this rectangle");
    }

    private bool DoesItIntersect(Rectangle rectangle)
    {
        foreach (var putRectangle in _putRectangles)
            if (putRectangle.IntersectsWith(rectangle)) return true;
        return false;
    }

    private Point GetInsertionPoint(Size rectangleSize, Point point, object rotateDirection)
    { 
        var insertionPoint = new Point(point.X, point.Y);
        if (rotateDirection.Equals(Additions.Models.RotateDirections.bottom))
            insertionPoint.X -= rectangleSize.Width;
        if (rotateDirection.Equals(Additions.Models.RotateDirections.left))
            insertionPoint.Y -= rectangleSize.Height;

        return insertionPoint;
    }

    private void AddVerticesToFreePoints(Rectangle rectangle)
    {
        for (var i = 0; i <= rectangle.Width; i += rectangle.Width)
        for (var j = 0; j <= rectangle.Height; j += rectangle.Height) 
            AddFreePoint(new Point(rectangle.X + i, rectangle.Y + j));
    }
    
    private void AddFreePoint(Point point)
    {
        var distanceFromCenter = CountDistanceFromCenter(point);
        if (!_nearestToTheCenterPoints.ContainsKey(distanceFromCenter))
            _nearestToTheCenterPoints.Add(distanceFromCenter, point);
    }
    
    private float CountDistanceFromCenter(Point point)
    {
        var distanceFromCenter = new Vector2(point.X - _tagCloudContainerConfig.Center.X, point.Y - _tagCloudContainerConfig.Center.Y);
        return distanceFromCenter.Length();
    }
}
