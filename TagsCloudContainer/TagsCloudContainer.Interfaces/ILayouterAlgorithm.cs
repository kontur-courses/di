using System.Drawing;

namespace TagsCloudContainer.Interfaces;

public interface ILayouterAlgorithm
{
    Rectangle PutNextRectangle(Size rectangleSize);
}