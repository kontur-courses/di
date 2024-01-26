namespace TagsCloudContainer.BuildingOptions;

public class DefaultDrawingOptionsProvider : IDrawingOptionsProvider
{
    public DrawingOptions DrawingOptions { get; }
    
    public DefaultDrawingOptionsProvider(DrawingOptions drawingOptions)
    {
        DrawingOptions = drawingOptions;
    }
}