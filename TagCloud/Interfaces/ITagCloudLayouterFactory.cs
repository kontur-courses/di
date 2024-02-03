using TagsCloudVisualization;
namespace TagCloud;

public interface ITagCloudLayouterFactory
{
    ITagCloudLayouter Get(IPoints points);
}