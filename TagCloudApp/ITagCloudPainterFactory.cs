using CircularCloudLayouter;

namespace TagCloudApp;

public interface ITagCloudPainterFactory
{
    TagCloudPainter Create(ITagCloudLayouter layouter);
}