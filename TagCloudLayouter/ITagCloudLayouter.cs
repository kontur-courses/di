using CircularCloudLayouter.Domain;

namespace CircularCloudLayouter;

public interface ITagCloudLayouter
{
    ImmutableRectangle PutNextRectangle(ImmutableSize rectSize);
}