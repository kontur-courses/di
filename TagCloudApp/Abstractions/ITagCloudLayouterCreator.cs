using CircularCloudLayouter;

namespace TagCloudApp.Abstractions;

public interface ITagCloudLayouterCreator
{
    public ITagCloudLayouter CreateLayouter();
}