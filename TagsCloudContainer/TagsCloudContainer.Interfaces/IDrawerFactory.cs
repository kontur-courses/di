using System.Drawing;

namespace TagsCloudContainer.Interfaces;

public interface IDrawerFactory
{
    (Func<Graphics, Func<ILayouterAlgorithm>, IDrawer>? drawer, bool success) Build(
        DrawerSettings drawerSettings);
}