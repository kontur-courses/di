namespace TagsCloudContainer;

public interface IDrawer
{
    void DrawCloud(IReadOnlyCollection<CloudWord> cloudWords);
}