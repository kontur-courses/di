using System.Drawing;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Abstractions;

public interface ILayouter : IService
{
    Rectangle PutNextRectangle(Size rectangleSize);
}