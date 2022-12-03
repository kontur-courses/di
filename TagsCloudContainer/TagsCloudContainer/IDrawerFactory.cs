using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public interface IDrawerFactory
{
    (Func<Graphics, Func<ILayouterAlgorithm>, IDrawer>? drawer, bool success) Build(
        DrawerSettings drawerSettings);
}