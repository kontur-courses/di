namespace TagsCloudVisualization.Drawer;

public interface IDrawer
{
    void Draw(IReadOnlyCollection<IDrawImage> drawImages,string filepath);
}