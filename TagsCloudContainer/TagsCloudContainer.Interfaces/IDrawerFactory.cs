namespace TagsCloudContainer.Interfaces;

public interface IDrawerFactory
{
    IDrawerProvider Build(
        DrawerSettings drawerSettings);
}