using CircularCloudLayouter;

namespace TagCloudCreator.Interfaces.Providers;

public interface ITagCloudLayouterProvider
{
    public ITagCloudLayouter CreateLayouter();
}