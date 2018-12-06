using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.Preprocessing
{
    public interface ITagCloudLayouterFactory
    {
        TagCloudLayouter Create();
    }
}