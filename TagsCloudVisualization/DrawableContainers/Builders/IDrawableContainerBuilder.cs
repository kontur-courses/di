namespace TagsCloudVisualization.DrawableContainers.Builders
{
    public interface IDrawableContainerBuilder
    {
        void AddTag(Tag tag);
        IDrawableContainer Build();
    }
}