namespace TagsCloudContainer.Interfaces;

public interface IDrawer
{
    void DrawCloud(IReadOnlyCollection<CloudWord> cloudWords);
}