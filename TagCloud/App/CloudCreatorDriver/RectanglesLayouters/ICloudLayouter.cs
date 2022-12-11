using System.Drawing;

namespace TagCloud.App.CloudCreatorDriver.RectanglesLayouters;

public interface ICloudLayouter
{
    void SetSettings(ICloudLayouterSettings settings);
    
    Rectangle PutNextRectangle(Size rectangleSize);
}