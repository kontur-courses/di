using TagsCloudVisualization.Drawable;

namespace TagsCloudVisualization.DrawableFactory
{
    public interface ITagDrawableFactory
    {
        TagDrawable Create(Tag tag);
    }
}